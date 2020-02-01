using crt_creditgw_auth_api.Creditgateway.scope.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> AddScope(ApiScopeBindingDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repo.CreateApiScope(_factory.BuildApiScope(dto)));
        }

        /// <summary>
        /// Adds a new Client Scope
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("clientScope/add")]
        public async Task<IActionResult> AddClientScope(ClientScopeBindingDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repo.CreateClientScope(_factory.BuildClientScope(dto)));
        }

    }
}