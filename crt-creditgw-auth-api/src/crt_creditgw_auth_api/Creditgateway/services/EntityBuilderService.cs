using crt_creditgw_auth_api.Creditgateway.services.claims;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.services
{
    public class EntityBuilderService<T>
    {
        private IClaimRepository _claimRepo;
        private IClaimFactory _claimFactory;
        public EntityBuilderService(IClaimRepository claimRepo, IClaimFactory claimFactory)
        {
            _claimRepo = claimRepo;
            _claimFactory= claimFactory;
        }
        public async Task<string> BuildEntity(dynamic model)
        {
            var type = typeof(T);
            //model = type;

            string result = "";
            if (IsApiClaim(type))
            {
                type = _claimFactory.CreateApiClaims(model);
                await Task.Run(() =>
                {
                    //JB. This is now invoking the correct Overloaded Method by passing ApiResourceClaim Entity
                    result = _claimRepo.AddClaim(model);
                });
            }
            else {
                type = _claimFactory.CreateClientClaims(model);
                await Task.Run(() =>
                {
                    //JB. This is now invoking the correct Overloaded Method by passing ClientClaim Entity
                    result = _claimRepo.AddClaim(model);
                });
            }
            return result;
        }

        public bool IsApiClaim(Type input)
        {
            
            return input.GetType() == typeof(ApiResourceClaim) ? true : false;
        }
    }
}
