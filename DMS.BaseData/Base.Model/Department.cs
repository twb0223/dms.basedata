using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseData.Model
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Department
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        
        public int DepartmentID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 所属项目ID
        /// </summary>
        [Required]
        public int ProjectID { get; set; }

        /// <summary>
        /// 所属项目实体
        /// </summary>
        public virtual Project Project { get; set; }
    }
}
