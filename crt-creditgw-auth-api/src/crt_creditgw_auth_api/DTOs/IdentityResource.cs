using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.DTOs
{
    public class IdentityResource:BaseDto
    {
        public bool Enabled { get; set; }// = table.Column<bool>(nullable: false),
        public string Name { get; set; }// = table.Column<string>(maxLength: 200, nullable: false),
        public string DisplayName { get; set; } //= table.Column<string>(maxLength: 200, nullable: true),
        public string Description { get; set; } //= table.Column<string>(maxLength: 1000, nullable: true),
        public bool Required { get; set; } //= table.Column<bool>(nullable: false),
        public bool Emphasize { get; set; } //= table.Column<bool>(nullable: false),
        public bool ShowInDiscoveryDocument { get; set; } //= table.Column<bool>(nullable: false),
        public DateTime Created { get; set; } //= table.Column<DateTime>(nullable: false),
        public DateTime Updated { get; set; } //= table.Column<DateTime>(nullable: true),
        public bool NonEditable { get; set; } //= table.Column<bool>(nullable: false)
    }
}
