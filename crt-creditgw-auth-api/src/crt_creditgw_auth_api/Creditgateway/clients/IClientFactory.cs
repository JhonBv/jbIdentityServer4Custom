﻿using crt_creditgw_auth_api.Creditgateway.clients.DTOs;
using IdentityServer4.EntityFramework.Entities;

namespace crt_creditgw_auth_api.Creditgateway.clients
{
    public interface IClientFactory
    {
        Client CreateClientEntity(ClientBindingDto dto);
        ClientSecret CreateClientSecret(int clientId, string secret);
    }
}