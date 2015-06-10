using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Model
{
    public class ProjectDepartmentRelation
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProjectDepartmentRelationsID { get; set; }


        public int? ProjectID { get; set; }


        public int? DepartmentID { get; set; }
    }
}
