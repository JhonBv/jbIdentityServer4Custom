using crt_creditgw_auth_api.Creditgateway.account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Models.Creditgateway.account
{
    public class LogoutViewModel : LogoutInputModel
    {
        public bool ShowLogoutPrompt { get; set; } = true;
    }
}
