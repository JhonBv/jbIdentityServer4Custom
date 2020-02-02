using crt_creditgw_auth_api.Creditgateway.services.claims.DTOs;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.services.claims
{
    public interface IClaimFactory
    {
        /// <summary>
        /// JB. Create Entity to add new set of Claims about a Client
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ClientClaim CreateClientClaims(ClientClaimBindingDto dto);

        /// <summary>
        /// JB. Create Entity to add new Api Resource Claims
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ApiResourceClaim CreateApiClaims(ApiClaimBindingDto dto);
    }
}
