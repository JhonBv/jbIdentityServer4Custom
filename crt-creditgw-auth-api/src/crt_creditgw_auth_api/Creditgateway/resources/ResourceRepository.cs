using crt_creditgw_auth_api.Creditgateway.services.secrets;
using crt_creditgw_auth_api.Creditgateway.services.secrets.DTOs;
using crt_creditgw_auth_api.Data;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.resources
{
    public class ResourceRepository:IResourceRepository
    {
        private ResourceConfigDbContext _ctx;
        public ResourceRepository(ResourceConfigDbContext context)
        {
            _ctx = context;
        }

        /// <summary>
        /// Add new ApiResource to Database.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>Int ID of newly created ApiResource</returns>
        public int CreateApiResource(ApiResource resource)
        {
            int daId;
            using (_ctx)
            {
                _ctx.ApiResources.Add(resource);
                _ctx.SaveChanges();
                daId = resource.Id;
            }
            return daId;
        }
    }
}
