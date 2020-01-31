using crt_creditgw_auth_api.Creditgateway.services.secrets.DTOs;

namespace crt_creditgw_auth_api.Creditgateway.services.secrets
{
    public class SecretsService:ISecretsService
    {
        private ISecretsRepository _repo;
        private ISecretsFactory _factory;
        public SecretsService(ISecretsFactory factory, ISecretsRepository repo)
        {
            _factory = factory;
            _repo = repo;
        }

        public string AddSecret(SecretBindingDto dto)
        {
            string response="";
            if (dto.AudienceType == "ApiResource")
            {
                response = _repo.AddApiSecret(_factory.buildApiSecret(dto));
            }
            else{
                response =_repo.AddClientSecret(_factory.buildClientSecret(dto));
            }
            return response;
        }
    }
}
