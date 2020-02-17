using crt_creditgw_auth_api.Data;
using crt_creditgw_auth_api.Models;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace crt_creditgw_auth_api.Factories
{
    public class UserProfileFactory
    {
        static AppUserDbContext _ctx;
        private UrlHelper _UrlHelper;
        private UserManager<ApplicationUser> _AppUserManager;

        public UserProfileFactory(HttpRequestMessage request, UserManager<ApplicationUser> appUserManager, AppUserDbContext context)
        {
            _UrlHelper = new UrlHelper(request);
            _AppUserManager = appUserManager;
            _ctx = context;
        }

        public UserReturnModel Create(ApplicationUser appUser)
        {
            return new UserReturnModel
            {
                Url = _UrlHelper.Link("GetUserById", new { id = appUser.Id }),
                Id = appUser.Id,
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
            };

        }
    }

    /// <summary>
    /// JB. Returns the created User with Login.
    /// </summary>
    public class UserReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        //public IList<string> Roles { get; set; }
        public IList<Claim> Claims { get; set; }

    }
}
