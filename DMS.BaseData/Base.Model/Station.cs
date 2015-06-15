using System;
using System.ComponentModel.DataAnnotations;

namespace BaseData.Model
{
    /// <summary>
    /// 点位
    /// </summary>
    public class Station
    {
        /// <summary>
        /// 点位ID
        /// </summary>
        [Key]
        public string StationID { get; set; }

        /// <summary>
        /// 点位名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string StationName { get; set; }

        /// <summary>
        /// 点位描述
        /// </summary>
        [MaxLength(150)]
        public string StationDes { get; set; }

        /// <summary>
        /// 设备状态：0：未分配；1：已分配
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Required]
        public int DepartmentID { get; set; }

        /// <summary>
        /// 部门对象
        /// </summary>
        public virtual Department Department { get; set; }

    }
}
