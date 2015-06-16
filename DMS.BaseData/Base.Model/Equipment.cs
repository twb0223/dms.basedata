using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BaseData.Model
{
    /// <summary>
    /// 设备
    /// </summary>
    public class Equipment
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [Key]
        public string EquipmentID { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string EquipmentName { get; set; }
        /// <summary>
        /// Mac地址
        /// </summary>
        [MaxLength(30)]
        [Required]
        public string EquipmentMac { get; set; }
        /// <summary>
        /// 设备CPU
        /// </summary>
        [MaxLength(20)]
        public string EquipmentCPU { get; set; }
        /// <summary>
        /// 设备存储介质
        /// </summary>
        [MaxLength(20)]
        public string EquipmentDisk { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public int EqumentType { get; set; }

        /// <summary>
        /// 设备操作系统
        /// </summary>
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

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }


    }
}
