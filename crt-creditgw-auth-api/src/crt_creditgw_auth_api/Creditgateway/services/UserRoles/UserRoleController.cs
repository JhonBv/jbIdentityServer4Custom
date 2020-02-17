using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using crt_creditgw_auth_api.Creditgateway.services.UserRoles.DTOs;
using crt_creditgw_auth_api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crt_creditgw_auth_api.Creditgateway.services.UserRoles
{
    [Route("api/role")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private AppUserDbContext _ctx;
        public UserRoleController(AppUserDbContext context)
        {
            _ctx = context;
        }

        [HttpPost]
        [Route("add")]
        public async Task<HttpResponseMessage> NewRole(RoleBindingDto model)
        {
            var my = new Microsoft.AspNetCore.Identity.IdentityRole { Name = model.Name, NormalizedName = model.Name.ToUpper() };
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.Created);
            await Task.Run(() => { 
            _ctx.Roles.Add(my);
            _ctx.SaveChanges();

                if (String.IsNullOrEmpty(my.Id))
                {
                    message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                }
            });
            return message;
        }
    }
}