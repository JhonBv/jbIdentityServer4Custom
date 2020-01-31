using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.scope.DTOs
{
    public class ClientScopeBindingDto
    {
        [Required]
        public string Scope { get; set; }
        [Required]
        public string Client_Id { get; set; }
    }
}
