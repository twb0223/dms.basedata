using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Base.Model
{
    public class Equement
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string EquipmentID { get; set; }

        [MaxLength(50)]
        [Required]
        public string EquipmentName { get; set; }

        [MaxLength(20)]
        [Required]
        public string EquipmentMac { get; set; }

        [MaxLength(20)]
        public string EquipmentCPU { get; set; }

        [MaxLength(20)]
        public string EquipmentDisk { get; set; }

        /// <summary>
        /// 设备信息变更标识，供通讯服务使用
        /// </summary>
        [MaxLength(40)]
        public string ClientChangeFlag { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? CreateBy { get; set; }


    }
}
