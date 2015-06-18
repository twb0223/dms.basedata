using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseData.Model
{
    public class RoleAuthority
    {
        [Key]
        [Required]
        public int RoleAuthorityID { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required]
        public string MenuJson { get; set; }

    }
}
