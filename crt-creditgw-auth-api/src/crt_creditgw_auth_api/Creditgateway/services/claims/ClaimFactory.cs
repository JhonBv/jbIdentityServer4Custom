using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crt_creditgw_auth_api.Creditgateway.services.claims.DTOs;
using IdentityServer4.EntityFramework.Entities;

namespace crt_creditgw_auth_api.Creditgateway.services.claims
{
    public class ClaimFactory : IClaimFactory
    {
        public ApiResourceClaim CreateApiClaims(ApiClaimBindingDto dto)
        {

            return new ApiResourceClaim
            { /*JB. Must pass int ID of the ApiresourceId*/
                ApiResourceId = dto.ApiResourceId,
                Type = dto.Type
            };
        }

        public ClientClaim CreateClientClaims(ClientClaimBindingDto dto)
        {
            return new ClientClaim
            { /*JB. Must pass int ID of the ClientId*/
                ClientId = dto.ClientId,
                Type = dto.Type,
                Value = dto.Value
            };
        }
    }
}
