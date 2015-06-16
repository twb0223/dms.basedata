using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseData.Model
{
    /// <summary>
    /// 项目
    /// </summary>
    public class Project
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProjectID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目类型ID
        /// </summary>
        [Required]
        public int ProjectTypeID { get; set; }
        /// <summary>
        /// 项目类型对象
        /// </summary>
        public virtual ProjectType ProjectType { get; set; }
    }
}
