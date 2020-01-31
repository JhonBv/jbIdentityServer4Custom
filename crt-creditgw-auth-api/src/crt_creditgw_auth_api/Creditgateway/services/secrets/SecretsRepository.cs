﻿using crt_creditgw_auth_api.Data;
using IdentityServer4.EntityFramework.Entities;
using System;

namespace crt_creditgw_auth_api.Creditgateway.services.secrets
{
    public class SecretsRepository : ISecretsRepository
    {
        public SecretsRepository()
        {
        }

        public string AddApiSecret(ApiSecret model)
        {
            string result;
            try
            {
                using (var _ctx = new ResourceConfigDbContext())
                {
                    _ctx.ApiSecrets.Add(model);
                    _ctx.SaveChanges();
                    //int Id = 
                    result = model.Id.ToString();
                }
                   
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }

        public string AddClientSecret(ClientSecret model)
        {
            string result;
            try
            {
                using (var _ctx = new ResourceConfigDbContext())
                {
                    _ctx.ClientSecrets.Add(model);
                    _ctx.SaveChanges();
                    //int Id = 
                    result = model.Id.ToString();
                }

            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }
    }
}
