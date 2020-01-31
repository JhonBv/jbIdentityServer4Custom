using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crt_creditgw_auth_api.Models
{
    /// <summary>
    /// Director listed by the Companies House.
    /// </summary>
    public class BusinessOfficer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// JB. Role within the organisation
        /// </summary>
        public string Role { get; set; }

        public DateTime DateAppointed { get; set; }

        public DateTime Resigned { get; set; }

        public string Source { get; set; }

    }
}
