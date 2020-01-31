using crt_creditgw_auth_api.Creditgateway.scope.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace crt_creditgw_auth_api.Creditgateway.scope
{
    [Route("api")]
    [ApiController]
    public class ScopeController : ControllerBase
    {
        private IScopeFactory _factory;
        private IScopeRepository _repo;
        
        public ScopeController(IScopeFactory factory, IScopeRepository repo)
        {

            _factory = factory;
            _repo = repo;
        }
        /// <summary>
        /// Adds a new API scope
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("apiScope/add")]
        public IActionResult AddScope(ApiScopeBindingDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_repo.CreateApiScope(_factory.BuildApiScope(dto)));
        }

        /// <summary>
        /// Adds a new Client Scope
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("clientScope/add")]
        public IActionResult AddClientScope(ClientScopeBindingDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_repo.CreateClientScope(_factory.BuildClientScope(dto)));
        }

    }
}