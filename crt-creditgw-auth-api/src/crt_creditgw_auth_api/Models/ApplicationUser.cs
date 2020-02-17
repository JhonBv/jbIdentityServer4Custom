using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Models
{
    public class ApplicationUser: IdentityUser
    {
        public bool ProfileCompleted { get; set; }
        public DateTime DateCreated { get; set; }
        public string OriginUrl { get; set; }
    }
}
