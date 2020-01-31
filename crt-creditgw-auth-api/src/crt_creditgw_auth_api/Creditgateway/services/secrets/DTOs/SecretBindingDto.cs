using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.services.secrets.DTOs
{
    public class SecretBindingDto
    {
        public string Description { get; set; }
        public string value { get; set; }//JB. To be Hashed
    
        public int ResourceId { get; set; }
        /// <summary>
        /// Client_Id for building a CLientSecret or ApiResource name for an ApiResourceSecret.
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// Tell me please what type are you? i.e. Client or ApiResource?
        /// </summary>
        public string AudienceType { get; set; }
    }
}
