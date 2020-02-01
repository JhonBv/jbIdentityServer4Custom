﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crt_creditgw_auth_api.Creditgateway.clients.DTOs;
using crt_creditgw_auth_api.Creditgateway.scope;
using crt_creditgw_auth_api.Creditgateway.scope.DTOs;
using crt_creditgw_auth_api.Creditgateway.services;
using crt_creditgw_auth_api.Creditgateway.services.secrets;
using crt_creditgw_auth_api.Creditgateway.services.secrets.DTOs;
using crt_creditgw_auth_api.Data;
using IdentityServer4.EntityFramework.Entities;

namespace crt_creditgw_auth_api.Creditgateway.clients
{
    public class ClientRepository : IClientRepository
    {
        private IClientFactory _factory;
        private ISecretsRepository _secretsRepo;
        private IScopeFactory _scopeFactory;
        private IScopeRepository _scopeRepo;
        
        
        public ClientRepository(IClientFactory factory, ISecretsRepository secretsRepo, IScopeFactory scopeFactory, IScopeRepository scopeRepo)
        {
            _factory = factory;
            _secretsRepo= secretsRepo;
            _scopeFactory = scopeFactory;
            _scopeRepo = scopeRepo;
        }

        /// <summary>
        /// JB. Asyncronously create a new Client. Add Secret and Scope and return a response witht he info needed for Client Integration.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task<ClientResponseDto> AddClient(Client client)
        {
            string NewSecret = await RandomStringGenerator.GeneratedString();

            ClientResponseDto response = null;
            int clientId = 0;
            await Task.Run(() =>
            {
                using (var ctx = new ResourceConfigDbContext())
                {
                    ctx.Clients.Add(client);
                    ctx.SaveChanges();
                    clientId = client.Id;
                };

                _secretsRepo.AddClientSecret(_factory.CreateClientSecret(clientId, NewSecret));

                response = new ClientResponseDto { 
                ClientName = client.ClientName,
                Client_Id = client.ClientId,
                Secret = NewSecret
                };

            });

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
