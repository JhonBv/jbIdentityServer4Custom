using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Models
{
    /// <summary>
    /// Company/Business regustered in the Companies House and for which a Credit Score is requested.
    /// </summary>
    public class BusinessProfile: BaseModel
    {
        [Required]
        public string CompanyNumber { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public UserAddress RegisteredOfficeAddress { get; set; }

        public UserAddress TradingAddress { get; set; }

        public List<BusinessOfficer> DirectorsList { get; set; }

        public List<BusinessCategory> BusinessCategory { get; set; }

        public List<SICCode> SicCode { get; set; }



    }
}
