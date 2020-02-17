using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crt_creditgw_auth_api.Data;
using IdentityServer4.EntityFramework.Entities;

namespace crt_creditgw_auth_api.Creditgateway.services.grants
{
    public class GrantTypeRepository : IGrantTypeRepository
    {
        private ResourceConfigDbContext ctx;
        public GrantTypeRepository(ResourceConfigDbContext  context)
        {
            ctx = context;
        }
        public async Task<string> AddGrantType(ClientGrantType grant)
        {
            string result = "";
            int grantId = 0;
            await Task.Run(()=> {
                using (/*var*/ ctx /*= new ResourceConfigDbContext()*/)
                {
                    ctx.ClientGrantTypes.Add(grant);
                    ctx.SaveChanges();
                    grantId = grant.Id;
                    result = "Grant " + grantId + "added successfully";
                }
            });
            return result;
        }

        public Task<List<ClientGrantType>> ListClientGrants(int clientId)
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveGrantType(int gId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateGrantType(ClientGrantType dto)
        {
            throw new NotImplementedException();
        }
    }
}
