using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Models
{
    public class UserAddress : BaseModel
    {
        [StringLength(100)]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        [StringLength(10, ErrorMessage = "A valid Post Code must be provided")]
        public string PostCode { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

    }
}
