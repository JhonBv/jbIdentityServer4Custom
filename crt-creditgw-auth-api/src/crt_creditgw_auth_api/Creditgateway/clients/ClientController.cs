using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crt_creditgw_auth_api.Creditgateway.clients.DTOs;
using crt_creditgw_auth_api.Data;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crt_creditgw_auth_api.Creditgateway.clients
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private IClientFactory _factory;
        private IClientRepository _repo;

        public ClientController(IClientFactory factory, IClientRepository repo)
        {

            _factory = factory;
            _repo = repo;
        }

        /// <summary>
        /// Create a new Client and persist it in DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponseDto))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Dto.Responses.ErrorResponse))]
        public async Task<IActionResult> AddClient(ClientBindingDto dto)
        {
            return Ok(await _repo.AddClient(_factory.CreateClientEntity(dto),dto));
        }
    }
}