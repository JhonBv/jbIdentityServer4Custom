using crt_creditgw_auth_api.Creditgateway.resources;
using crt_creditgw_auth_api.Creditgateway.services.secrets.DTOs;
using IdentityServer4.EntityFramework.Entities;

namespace crt_creditgw_auth_api.Creditgateway.services.secrets
{
    /// <summary>
    /// Build a Secret type either for ApiSecret or ClientSecret
    /// </summary>
    public interface ISecretsFactory
    {
        ClientSecret buildClientSecret(SecretBindingDto dto);
        ApiSecret buildApiSecret(SecretBindingDto dto);

        SecretBindingDto BuildApiSecretBinding(ResourceBindingDto dto, int daId, string type);

    }
}
