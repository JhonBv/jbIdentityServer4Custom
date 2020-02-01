using IdentityServer4.EntityFramework.Entities;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.scope
{
    public interface IScopeRepository
    {
        Task<string> CreateApiScope(ApiScope scope);
        Task<string> CreateClientScope(ClientScope scope);
    }
}
