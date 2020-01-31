using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.services.secrets
{
    public interface ISecretsRepository
    {
        string AddClientSecret(ClientSecret model);
        string AddApiSecret(ApiSecret model);
    }
}
