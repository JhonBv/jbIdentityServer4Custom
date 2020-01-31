using crt_creditgw_auth_api.Creditgateway.services.secrets.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.services.secrets
{
    public interface ISecretsService
    {
        string AddSecret(SecretBindingDto dto);
    }
}
