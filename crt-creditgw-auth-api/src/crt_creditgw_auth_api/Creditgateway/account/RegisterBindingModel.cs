using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.account
{
    public class RegisterBindingModel
    {
        [Required]
       public string UserName { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }//MUST BE .Sha256(),

        public string PhoneNumber { get; set; }
        
    }
}
