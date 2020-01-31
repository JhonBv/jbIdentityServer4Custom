using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.scope.DTOs
{
    public class ApiScopeBindingDto
    {
        [Required]
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int ApiResourceId { get; set; }
        
    }
}
