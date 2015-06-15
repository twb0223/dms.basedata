using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseData.Model
{
    /// <summary>
    /// 项目类型
    /// </summary>
    public class ProjectType
    {
        /// <summary>
        /// 项目类型ID
        /// </summary>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProjectTypeID { get; set; }
        /// <summary>
        /// 项目类型名称
        /// </summary>
        [Required]
        public string ProjectTypeName { get; set; }
    }
}
