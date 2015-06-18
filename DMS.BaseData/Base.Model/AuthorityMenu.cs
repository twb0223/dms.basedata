using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseData.Model
{
    public class AuthorityMenu
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string MenuCode { get; set; }

        [Required]
        public string MenuName { get; set; }

        public string ParentMenuCode { get; set; }

        public string URL { get; set; }

        [Required]
        public int ProjectID { get; set; }

        public virtual Project Project { get; set; }
    }
}
