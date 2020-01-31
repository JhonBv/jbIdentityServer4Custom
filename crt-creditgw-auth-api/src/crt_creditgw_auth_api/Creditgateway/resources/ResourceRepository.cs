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
        public ResourceRepository()
        {
        }

        /// <summary>
        /// Add new ApiResource to Database.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>Int ID of newly created ApiResource</returns>
        public int CreateApiResource(ApiResource resource)
        {
            int daId;
            using (var ctx = new ResourceConfigDbContext())
            {
                ctx.ApiResources.Add(resource);
                ctx.SaveChanges();
                daId = resource.Id;
            }
            return daId;
        }
    }
}
