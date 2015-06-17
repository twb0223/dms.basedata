using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseData.Model
{
    public class Role
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RoleID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; }

        /// <summary>
        /// 所属项目
        /// </summary>
        [Required]
        public int ProjectID { get; set; }

        /// <summary>
        /// 项目对象
        /// </summary>
        public virtual Project Project { get; set; }

    }
}
