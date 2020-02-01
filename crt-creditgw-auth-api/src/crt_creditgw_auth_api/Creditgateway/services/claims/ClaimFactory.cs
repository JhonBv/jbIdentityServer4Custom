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
        public async Task<ApiResourceClaim> CreateApiClaims(ApiClaimBindingDto dto, int aId)
        {
            ApiResourceClaim apiClaim = null;
            await Task.Run(() => { 
                apiClaim = new ApiResourceClaim { /*JB. Must pass int ID of the ApiresourceId*/
                ApiResourceId = aId,
                Type = dto.Type
                }; 
            });
            return apiClaim;
        }

        public async Task<ClientClaim> CreateClientClaims(ClientClaimBindingDto dto, int cId)
        {
            ClientClaim clientClaim = null;
            await Task.Run(() => { 
                clientClaim = new ClientClaim { /*JB. Must pass int ID of the ClientId*/
                ClientId=cId,
                Type = dto.Type,
                Value = dto.Value
                };
            });
            return clientClaim;
        }
    }
}
