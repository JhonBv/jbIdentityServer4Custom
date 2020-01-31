using crt_creditgw_auth_api.Creditgateway.scope.DTOs;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.scope
{
    public interface IScopeFactory
    {
        ApiScope BuildApiScope(ApiScopeBindingDto dto);
        ClientScope BuildClientScope(ClientScopeBindingDto dto);
    }
}
