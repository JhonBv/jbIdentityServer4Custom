// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using crt_creditgw_auth_api.Models;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace crt_creditgw_auth_api
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("BankConnector", "Bank Connector"),
                new ApiResource("RiskCalc", "Risk Calc"),
                new ApiResource("CDRScoring", "Credit Passport Score")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
    {
        new Client
        {
            ClientId = "client",

            // no interactive user, use the clientid/secret for authenticatio
            AllowedGrantTypes = GrantTypes.ClientCredentials,

            // secret for authentication
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },

            // scopes that client has access to
            AllowedScopes = { "BankConnector" }
        },

        new Client
        {
            ClientId = "creditpassport",

            // no interactive user, use the clientid/secret for authenticatio
            AllowedGrantTypes = GrantTypes.ClientCredentials,

            // secret for authentication
            ClientSecrets =
            {
                new Secret("D@S3cr3t".Sha256()),
                new Secret("My2ndS3cr3t".Sha256())
            },

            RedirectUris           = { "https://creditpassport.com/signin-oidc" },
            PostLogoutRedirectUris = { "https://creditpassport.com/signout-callback-oidc" },

            // scopes that client has access to
            AllowedScopes = { "BankConnector", "RiskCalk", "CDRScoring" }
        } 
            };


        public static IEnumerable<ApplicationUser> Users =>
            new List<ApplicationUser>
            { 
                new ApplicationUser{
                UserName="jhonbv",
                Email = "jhonb@me.com",
                EmailConfirmed = true,
                PasswordHash = "MyN!ceP@ss".Sha256(),
                NormalizedEmail ="jhonb@me.com",
                ProfileCompleted =false,
                NormalizedUserName ="jhonbv",
                PhoneNumber="07123487478",
                PhoneNumberConfirmed = true
                },
                new ApplicationUser{
                UserName="jhonb",
                Email = "jhonb+1@me.com",
                EmailConfirmed = true,
                PasswordHash = "MyN!ceP@ss".Sha256(),
                NormalizedEmail ="jhonb+1@me.com",
                ProfileCompleted =false,
                NormalizedUserName ="jhonb",
                PhoneNumber="07123487478",
                PhoneNumberConfirmed = true
                    
                }
            };
    }
}