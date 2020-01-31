using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.DTOs
{
    public class BaseDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //= table.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
    }
}
