using crt_creditgw_auth_api.Creditgateway.services.grants.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace crt_creditgw_auth_api.Creditgateway.services.grants
{
    public interface IGrantTypeRepository
    {
        Task<string> AddGrantType(ClientGrantType grant);
        Task<List<ClientGrantType>> ListClientGrants(int clientId);

        Task<string> UpdateGrantType(ClientGrantType grant);
        Task<string> RemoveGrantType(int gId);
    }
}
