using IdentityServer4.EntityFramework.Entities;
using System.Linq;
using crt_creditgw_auth_api.Data;
using crt_creditgw_auth_api.Creditgateway.scope.DTOs;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.scope
{
    public class ScopeFactory:IScopeFactory
    {

        ResourceConfigDbContext _ctx;
        public ScopeFactory()
        {
            _ctx = new ResourceConfigDbContext();
        }

        /// <summary>
        /// Add ApiScope
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ApiScope BuildApiScope(ApiScopeBindingDto dto)
        {
            var scope = new ApiScope {
                Name = dto.Name,
                DisplayName=dto.DisplayName,
                Description = dto.Description,
                ApiResourceId = dto.ApiResourceId,
                
                Emphasize = true
            };

            return scope;
        }
        /// <summary>
        /// Create ClientScope
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>ClientScope</returns>
        public ClientScope BuildClientScope(ClientScopeBindingDto dto)
        {
            //JB. Get juts the client ID (int)
            var clientId = _ctx.Clients.Where(a => a.ClientId == dto.Client_Id).FirstOrDefault();
            var scope = new ClientScope
            {
                Scope = dto.Scope,
                ClientId = clientId.Id
            };

            return scope;
        }

        public async Task<ClientScope> BuildClientScope(int ClientId, string scope)
        {
            ClientScope result = null;
            await Task.Run(() => {

                result = new ClientScope { 
                ClientId = ClientId,
                Scope = scope
                };
            });
            return result;
        }
    }
}
