using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace crt_creditgw_auth_api.Creditgateway.resources
{
    public class ResourceFactory : IResourceFactory
    {
        public List<ApiResource> ApiResources()
        {
            throw new NotImplementedException();
        }

        public ApiResource BuildApiResource(ResourceBindingDto dto)
        {
            return new ApiResource
            {
                Description = dto.Description,
                Name = dto.Name,
                DisplayName = dto.DisplayName,
                Enabled = true
            };
        }

        public ApiResource PatchApiResource(int resourceId)
        {
            throw new NotImplementedException();
        }
    }
}
