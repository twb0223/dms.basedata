using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseData.Model
{
    public class EquipmentType
    {
        /// <summary>
        /// 设备类型ID
        /// </summary>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int EquipmentTypeID { get; set; }
        /// <summary>
        /// 设备类型名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string EquipmentTypeName { get; set; }
    }
}
