using IdentityServer4.EntityFramework.Entities;

namespace crt_creditgw_auth_api.Creditgateway.scope
{
    public interface IScopeRepository
    {
        string CreateApiScope(ApiScope scope);
        string CreateClientScope(ClientScope scope);
    }
}
