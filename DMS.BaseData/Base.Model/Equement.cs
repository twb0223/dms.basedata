using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Base.Model
{
    public class Equement
    {
        [Key]
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


        public int EqumentType { get; set; }

        public int OsType { get; set; }

        /// <summary>
        /// 设备状态：0：未分配；1：已分配
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 设备信息变更标识，供通讯服务使用
        /// </summary>
        [MaxLength(40)]
        public string ClientChangeFlag { get; set; }

        public DateTime? CreateTime { get; set; }

        public string CreateBy { get; set; }


    }
}
