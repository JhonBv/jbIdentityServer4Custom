﻿using crt_creditgw_auth_api.Data;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.scope
{
    /// <summary>
    /// Persist Scopes in Database.
    /// </summary>
    public class ScopeRepository:IScopeRepository
    {
        private ResourceConfigDbContext ctx;
        public ScopeRepository(ResourceConfigDbContext context)
        {
            ctx = context;
        }
        /// <summary>
        /// JB. Create a New ApiScope when a new ApiResource is added to the Database.
        /// </summary>
        /// <param name="scope">ApiScope Type</param>
        /// <returns>String Result</returns>
        public async Task<string> CreateApiScope(ApiScope scope)
        {
            string result="";
            try
            {
                await Task.Run(() =>
                {
                    using (ctx)
                    {
                        ctx.ApiScopes.Add(scope);
                        ctx.SaveChanges();
                        result = "Api scope successfully added";
                    }
                });
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }

        /// <summary>
        /// JB. Creates a Client Socpe when a new Client is created in the Database.
        /// </summary>
        /// <param name="scope">ClientScope Type</param>
        /// <returns>String Result</returns>
        public async Task<string> CreateClientScope(ClientScope scope)
        {
            string result="";
            
            try
            {
                await Task.Run(() =>
                {
                    using (ctx)
                    { 
                        ctx.ClientScopes.Add(scope);
                        ctx.SaveChanges();
                        var id = scope.Id;
                        result = "Scope added for client Id " + id.ToString();
                    }
                });
            }
            catch (Exception ex)
            {
               result = ex.Message.ToString();
            }

            return result;
        }
    }
}
