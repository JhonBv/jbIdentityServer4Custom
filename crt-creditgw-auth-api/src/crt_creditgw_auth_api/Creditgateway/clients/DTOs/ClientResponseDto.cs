using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.clients.DTOs
{
    public class ClientResponseDto
    {
        public string Client_Id { get; set; }
        public string ClientName { get; set; }
        public string Secret { get; set; }
        public List<string> AllowedScopes { get; set; }
        public List<Dictionary<string, string>> Claims { get; set; }
    }
}
