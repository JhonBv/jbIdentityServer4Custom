using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.DTOs
{
    public class ApiClaim : BaseDto
    {
        public string Type { get;set;} //= table.Column<string>(maxLength: 200, nullable: false),
        public int ApiResourceId { get; set; } // = table.Column<int>(nullable: false)
    }
}
