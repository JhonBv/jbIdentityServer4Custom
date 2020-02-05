using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crt_creditgw_auth_api.Creditgateway.clients.DTOs;
using crt_creditgw_auth_api.Creditgateway.scope;
using crt_creditgw_auth_api.Creditgateway.services;
using crt_creditgw_auth_api.Creditgateway.services.claims;
using crt_creditgw_auth_api.Creditgateway.services.grants;
using crt_creditgw_auth_api.Creditgateway.services.secrets;
using crt_creditgw_auth_api.Data;
using IdentityServer4.EntityFramework.Entities;

namespace crt_creditgw_auth_api.Creditgateway.clients
{
    public class ClientRepository : IClientRepository
    {
        private IClientFactory _factory;
        private ISecretsRepository _secretsRepo;
        private IScopeRepository _scopeRepo;
        private IClaimRepository _claimRepo;
        private IGrantTypeRepository _grantRepo;
        
        
        public ClientRepository(
            IClientFactory factory, 
            ISecretsRepository secretsRepo, 
            IScopeRepository scopeRepo, 
            IClaimRepository claimRepo,
            IGrantTypeRepository grantRepo
            )
        {
            _factory = factory;
            _secretsRepo= secretsRepo;
            _scopeRepo = scopeRepo;
            _claimRepo = claimRepo;
            _grantRepo = grantRepo;
        }

        /// <summary>
        /// JB. Asyncronously create a new Client. Add Secret and Scope and return a response witht he info needed for Client Integration.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task<ClientResponseDto> AddClient(Client client, ClientBindingDto dto)
        {
            //JB. Build a newly randomdized Secret, this is what is passed to the client and it is not hashed yet. It will be hashed at persisting time.
            string NewSecret = await RandomStringGenerator.GeneratedString();

            ClientResponseDto response = null;
            int clientId = 0;

            try
            {
                await Task.Run(async () =>
            {

                using (var ctx = new ResourceConfigDbContext())
                {
                    ctx.Clients.Add(client);
                    ctx.SaveChanges();
                    clientId = client.Id;
                };
                //JB. Add now Secret
                _secretsRepo.AddClientSecret(_factory.CreateClientSecret(clientId, NewSecret));

                //JB. Add Client Grant Type
                await _grantRepo.AddGrantType(new ClientGrantType {ClientId = clientId, GrantType = IdentityServer4.Models.GrantType.ClientCredentials});

                //JB. Add Scopes this client is allowed in the system.
                foreach (var scopev in dto.AllowedScopes)
                {
                    await _scopeRepo.CreateClientScope(new ClientScope { ClientId = clientId, Scope = scopev });
                }

                response = new ClientResponseDto
                {
                    ClientName = client.ClientName,
                    Client_Id = client.ClientId,
                    Secret = NewSecret,
                    AllowedScopes = dto.AllowedScopes,
                    Claims = dto.Claims
                };
                //JB. Add claims. Info about this Client
                foreach (var c in dto.Claims)
                {
                    await _claimRepo.AddClaim(new ClientClaim { ClientId = clientId, Type = c["Type"], Value = c["Value"] });
                }

            });
            }
            catch (Exception ex)
            {
                ClientErrorResponseDto errorResponse = new ClientErrorResponseDto { Error = "Not Found. " + ex.Message };
            }

            return response;
        }

        public ICollection<Client> Clients()
        {
            throw new NotImplementedException();
        }

        public Client FindClientById(string Client_Id)
        {
            throw new NotImplementedException();
        }

        public void RemoveClient(int Id)
        {
            throw new NotImplementedException();
        }

        public Client UpdateClient(Client dto)
        {
            throw new NotImplementedException();
        }
    }
}
