using Microsoft.AspNetCore.Mvc;
using crt_creditgw_auth_api.Data;
using crt_creditgw_auth_api.Creditgateway.services.secrets;
using crt_creditgw_auth_api.Creditgateway.services.secrets.DTOs;

namespace crt_creditgw_auth_api.Creditgateway.resources
{
    [Route("api/resource")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private ISecretsService _secrets;
        private ISecretsFactory _secretFactory;
        private IResourceFactory _resourceFactory;
        private IResourceRepository _resourceRepo;

        public ResourceController(ISecretsService secrets, IResourceRepository resourceRepo, IResourceFactory resourceFactory, ISecretsFactory secretFactory)
        {
            _secrets = secrets;
            _resourceFactory = resourceFactory;
            _resourceRepo = resourceRepo;
            _secretFactory = secretFactory;
        }

        /// <summary>
        /// Create a new ApiResource and Secret.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("new")]
        public IActionResult createResource(ResourceBindingDto dto)
        {
            int daId = _resourceRepo.CreateApiResource(_resourceFactory.BuildApiResource(dto));

            if (!string.IsNullOrEmpty(daId.ToString()))
            {
                //JB. Add Secret
                _secrets.AddSecret(_secretFactory.BuildApiSecretBinding(dto, daId, "ApiResource"));
            }
            return Ok("Success " + daId );
        }
    }
}