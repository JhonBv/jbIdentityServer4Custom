using crt_creditgw_auth_api.Creditgateway.clients.DTOs;
using IdentityServer4.EntityFramework.Entities;

using System;

namespace crt_creditgw_auth_api.Factories
{
    public class ClientFactory
    {
        /// <summary>
        /// Builds a Client Entity.
        /// </summary>
        /// <param name="dto">ClientBindingDto</param>
        /// <returns></returns>
        public Client client(ClientBindingDto dto)
        {
            var FullEntity = new Client {
            //JB. Assign a new random ID.
            ClientId = Guid.NewGuid().ToString("N"),
            ClientName = dto.ClientName,
            Description = dto.Description,
            ClientUri = dto.ClientUri,
            LogoUri = dto.LogoUri,
            Enabled = true,

            };



            return FullEntity;
        }
    }
}
