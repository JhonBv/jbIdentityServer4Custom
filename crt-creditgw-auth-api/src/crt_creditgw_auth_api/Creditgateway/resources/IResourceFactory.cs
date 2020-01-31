using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.resources
{
    public interface IResourceFactory
    {
        ApiResource BuildApiResource(ResourceBindingDto dto);
        List<ApiResource> ApiResources();

        ApiResource PatchApiResource(int resourceId);

    }
}
