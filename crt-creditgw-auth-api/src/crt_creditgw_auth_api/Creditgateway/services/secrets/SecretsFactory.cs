using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crt_creditgw_auth_api.Creditgateway.resources;
using crt_creditgw_auth_api.Creditgateway.services.secrets.DTOs;
using crt_creditgw_auth_api.Data;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;

namespace crt_creditgw_auth_api.Creditgateway.services.secrets
{
    public class SecretsFactory : ISecretsFactory
    {
        public SecretsFactory()
        {
        }
        

        public ClientSecret buildClientSecret(SecretBindingDto dto) {

            return new ClientSecret {
                
                ClientId = dto.ResourceId,
                Type = "SharedSecret",
                Value = dto.value.Sha256(),
                Created = DateTime.Now,
                Description = dto.Description
            };
        }
        public ApiSecret buildApiSecret(SecretBindingDto dto)
        {

            return new ApiSecret
            {
                ApiResourceId = dto.ResourceId,
                Type = "SharedSecret",
                Value = dto.value.Sha256(),
                Created = DateTime.Now,
                Description = dto.Description
            };
        }

        public SecretBindingDto BuildApiSecretBinding(ResourceBindingDto dto, int daId, string type)
        {
            return new SecretBindingDto
            {
                ResourceId = daId,
                value = dto.Secret,
                Audience = dto.Name,
                AudienceType = type,
                Description = "Secrets for " + dto.Name
            };
        }
    }
}
