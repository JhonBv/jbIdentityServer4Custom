using crt_creditgw_auth_api.Creditgateway.clients.DTOs;
using IdentityServer4.EntityFramework.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.clients
{
    public interface IClientRepository
    {
        Task<ClientResponseDto> AddClient(Client client, ClientBindingDto dto);
        ICollection<Client> Clients();
        Client UpdateClient(Client dto);
        Client FindClientById(string Client_Id);
        void RemoveClient(int Id);

    }
}
