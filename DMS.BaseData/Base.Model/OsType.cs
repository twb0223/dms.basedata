using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseData.Model
{
    public class OsType
    {
        /// <summary>
        /// 操作系统类型ID
        /// </summary>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int OsTypeID { get; set; }
        /// <summary>
        /// 操作系统类型名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string OsTypeName { get; set; }
    }
}
