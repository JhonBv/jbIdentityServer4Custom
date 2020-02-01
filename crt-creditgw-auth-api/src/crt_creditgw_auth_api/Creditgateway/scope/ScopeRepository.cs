using crt_creditgw_auth_api.Data;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.scope
{
    public class ScopeRepository:IScopeRepository
    {
        private ResourceConfigDbContext _context;
        public ScopeRepository()
        {
            _context = new ResourceConfigDbContext();
        }

        public string CreateApiScope(ApiScope scope)
        {
            string result;
            try
            {
                _context.ApiScopes.Add(scope);
                Save();
                result = "Api scope successfully added";
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }

        public async Task<string> CreateClientScope(ClientScope scope)
        {
            string result="";
            
            try
            {
                await Task.Run(() =>
                {
                    _context.ClientScopes.Add(scope);
                    var id = scope.Id;
                    Save();
                    result = "Scope added for client Id " + id.ToString();
                });
            }
            catch (Exception ex)
            {
               result = ex.Message.ToString();
            }

            return result;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
