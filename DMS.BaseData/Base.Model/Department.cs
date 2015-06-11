using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Model
{
    public class Department
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DepartmentID { get; set; }

        [MaxLength(50)]
        [Required]
        public string DepartmentName { get; set; }

        [Required]
        public int ProjectID { get; set; }

        public virtual Project Project { get; set; }
    }
}
