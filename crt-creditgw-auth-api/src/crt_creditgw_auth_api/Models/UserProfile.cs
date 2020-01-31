using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Models
{
    public class UserProfile:BaseModel
    {

        public string CompanyName { get; set; }

        public string VatNumber { get; set; }

        [Required]
        public string Title { get; set; }


        [Required]
        [StringLength(100)]
        public string UserFirstName { get; set; }

        [StringLength(100)]
        public string UserMiddleName { get; set; }

        [StringLength(100)]
        public string UserLastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name="Secondary Email Address")]
        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string UserDob { get; set; }


        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        //public UserAddress Address { get; set; }
    }
}
