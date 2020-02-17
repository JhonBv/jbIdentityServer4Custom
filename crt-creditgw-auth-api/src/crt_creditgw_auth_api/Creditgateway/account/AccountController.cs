using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
//using System.Net.Http;
using System.Threading.Tasks;
using crt_creditgw_auth_api.Data;
using crt_creditgw_auth_api.Factories;
using crt_creditgw_auth_api.Models;
using crt_creditgw_auth_api.Models.Creditgateway.account;
using crt_creditgw_auth_api.Models.ViewModels;
using crt_creditgw_auth_api.Services;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;

namespace crt_creditgw_auth_api.Creditgateway.account
{
    [Microsoft.AspNetCore.Mvc.Route("api/user")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private AppUserDbContext _ctx;

        public IConfiguration Configuration { get; }
        public AccountController(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            AppUserDbContext context)
        //JB. Constructor Body
        {
            _ctx = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;

        }

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // build a model so we know what to show on the login page
            var vm = await BuildLoginViewModelAsync(returnUrl);

            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return RedirectToAction("Challenge", "External", new { provider = vm.ExternalLoginScheme, returnUrl });
            }

            return Ok(vm);
        }



        [HttpPost]
        [Route("register")][Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<HttpResponseMessage> Register(ApplicationUser model)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("Model invalid");
            }
            //JB. if user has not send username, then set the Email as username.
            var user = new ApplicationUser() { UserName = model.UserName = String.IsNullOrEmpty(model.UserName) ? model.Email : model.UserName, Email = model.Email, OriginUrl = model.OriginUrl, DateCreated= DateTime.Now };
            
            IdentityResult result = await _userManager.CreateAsync(user, model.PasswordHash.ToSha256());

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.ToString());
            }

            string code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, code, email = user.Email }));
            string babody = "<h3>Hello " + user.UserName + " and welcome to the Credit Passport</h3> <p style=\"color:black; font-weight: bold;\">In order to continue with your registration Please confirm your email address by clicking <a href=\"" + callbackUrl + "\">here</a></p>";
            
            EmailService email = new EmailService();
            await email.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage { Destination = user.Email, Subject="Confirm Email", Body= babody});
                

            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Webpage,user.OriginUrl));
            await _userManager.AddToRoleAsync(user, "User");


            //JB. Return generated UserId to client
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        [Route("GetUserById")]
        public ApplicationUserViewModel GetUser(string userId) {

            var res = _ctx.Users.Where(u => u.Id == userId).FirstOrDefault();
            return new ApplicationUserViewModel {
                Id = res.Id,
                Email = res.Email,
                EmailConfirmed =res.EmailConfirmed
            };
        }

        [AllowAnonymous]
        [HttpGet]
        /// <summary>
        /// End point returning the UserCode after user activaytes user email address.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<HttpResponseMessage> ConfirmEmail(string userId = "", string code = "")
        {

            var response = new HttpResponseMessage();
            var badUriResponse = "https://www.creditpassport.com/oops";
            var goodUriResponse = "https://www.creditpassport.com/thanks";
            var daUser = _ctx.Users.Where(u => u.Id == userId).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                response.Headers.Location =  new Uri(badUriResponse);
                return response;
            }

            IdentityResult result = await this._userManager.ConfirmEmailAsync(daUser, code);

            if (result.Succeeded)
            {
                response.Headers.Location = new Uri(goodUriResponse);

                return response;
            }
            else
            {
                response.Headers.Location = new Uri(badUriResponse);
                return response;
            }
        }


        /*****************************************/
        /* helper APIs for the AccountController */
        /*****************************************/

        protected HttpResponseMessage GetErrorResult(IdentityResult result)
        {
            var response = Request.HttpContext.Response;
            if (result == null)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);  //InternalServerError(); //"500";
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var i in result.Errors)
                    {
                        ModelState.AddModelError("", i.Description);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }

                return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            }

            return null;
        }


        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Username = context?.LoginHint,
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null ||
                            (x.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase))
                )
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        //private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        //{
        //    // get context information (client name, post logout redirect URI and iframe for federated signout)
        //    var logout = await _interaction.GetLogoutContextAsync(logoutId);

        //    var vm = new LoggedOutViewModel
        //    {
        //        AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
        //        PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
        //        ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
        //        SignOutIframeUrl = logout?.SignOutIFrameUrl,
        //        LogoutId = logoutId
        //    };

        //    if (User?.Identity.IsAuthenticated == true)
        //    {
        //        var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
        //        if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
        //        {
        //            var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
        //            if (providerSupportsSignout)
        //            {
        //                if (vm.LogoutId == null)
        //                {
        //                    // if there's no current logout context, we need to create one
        //                    // this captures necessary info from the current logged in user
        //                    // before we signout and redirect away to the external IdP for signout
        //                    vm.LogoutId = await _interaction.CreateLogoutContextAsync();
        //                }

        //                vm.ExternalAuthenticationScheme = idp;
        //            }
        //        }
        //    }

        //    return vm;
        //}
    }
}