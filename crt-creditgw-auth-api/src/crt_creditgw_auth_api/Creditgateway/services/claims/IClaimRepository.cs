using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.services.claims
{
    public interface IClaimRepository
    {
        Task<string> AddClaim(ClientClaim claim);
        Task<string> AddClaim(ApiResourceClaim claim);
        Task<string> UpdateClaim(ClientClaim claim);
        Task<string> UpdateClaim(ApiResourceClaim claim);
        Task<ICollection<ClientClaim>> ListClientClaims();
        Task<ICollection<ApiResourceClaim>> ListApiClaims();

        Task<ClientClaim> GetClientClaimById(int claimId);
        Task<ApiResourceClaim> GetApiClaimById(int claimId);

        Task<string> RemoveClientClaim(int claimId);
        Task<string> RemoveApiClaim(int claimId);
    }
}
