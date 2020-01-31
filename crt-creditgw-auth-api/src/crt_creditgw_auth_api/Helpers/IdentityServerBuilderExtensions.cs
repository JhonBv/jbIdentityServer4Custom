using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;

namespace crt_creditgw_auth_api.Helpers
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddAdApiResources(this IIdentityServerBuilder builder, IEnumerable<ApiResource> apiResources)
        {
            builder.Services.AddSingleton(apiResources);
            //builder.AddResourceStore<AdResourcesStore>();
            return builder;
        }
        public static IIdentityServerBuilder AddAdIdentityResources(this IIdentityServerBuilder builder, IEnumerable<IdentityResource> identityResources)
        {
            builder.Services.AddSingleton(identityResources);
            //builder.AddAdIdentityResources<T>;
            return builder;
        }
    }
}
