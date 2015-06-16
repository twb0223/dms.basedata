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
    /// 设备点位关系
    /// </summary>
    public class EqumentStation
    {
        /// <summary>
        /// 设备点位关系ID
        /// </summary>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>
        [Required]
        public string EquipmentID { get; set; }

        /// <summary>
        /// 设备对象
        /// </summary>
        public virtual Equipment Equipment { get; set; }

        /// <summary>
        /// 点位ID
        /// </summary>
        [Required]
        public string StationID { get; set; }
        /// <summary>
        /// 点位对象
        /// </summary>
        public virtual Station Station { get; set; }

    }
}
