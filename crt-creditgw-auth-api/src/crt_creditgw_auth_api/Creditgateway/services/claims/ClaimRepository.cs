using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crt_creditgw_auth_api.Data;
using IdentityServer4.EntityFramework.Entities;

namespace crt_creditgw_auth_api.Creditgateway.services.claims
{
    public class ClaimRepository : IClaimRepository
    {
        /// <summary>
        /// Add new Client Claim
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        public async Task<string> AddClaim(ClientClaim claim)
        {
            string result = "";
            await Task.Run(()=> {
                try {
                    using (var ctx = new ResourceConfigDbContext()) {

                        ctx.ClientClaims.Add(claim);
                        ctx.SaveChanges();
                        result = "ClaimId "+ claim.Id.ToString();
                    }
                }
                catch (Exception ex) { 
                    result = ex.Message; }
            });
            return result;
        }
        /// <summary>
        /// Add new Api Resource Claim
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        public async Task<string> AddClaim(ApiResourceClaim claim)
        {
            string result = "";
            await Task.Run(() => {
                try {

                    using (var ctx = new ResourceConfigDbContext())
                    {
                        ctx.ApiClaims.Add(claim);
                        ctx.SaveChanges();
                        result = "ApiClaimId " + claim.Id.ToString();
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            });
            return result;
        }

        public Task<ApiResourceClaim> GetApiClaimById(int claimId)
        {
            throw new NotImplementedException();
        }

        public Task<ClientClaim> GetClientClaimById(int claimId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ApiResourceClaim>> ListApiClaims()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ClientClaim>> ListClientClaims()
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveApiClaim(int claimId)
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveClientClaim(int claimId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateClaim(ClientClaim claim)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateClaim(ApiResourceClaim claim)
        {
            throw new NotImplementedException();
        }
    }
}
