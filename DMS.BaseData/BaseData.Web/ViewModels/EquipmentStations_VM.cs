using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseData.Web.ViewModels
{
    public class EquipmentStations_VM
    { 
        /// <summary>
        /// 设备ID
        /// </summary>
        public string EquipmentID { get; set; }

        /// <summary>
        /// 点位ID
        /// </summary>
        public string StationID { get; set; }
     
        /// <summary>
        /// 操作系统
        /// </summary>
        public string OsTypeName { get; set; }

        /// <summary>
        /// 设备类别
        /// </summary>
        public string EquipmentTypeName { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; set; }
        /// <summary>
        /// 设备mac地址
        /// </summary>
        public string EquipmentMac { get; set; }
        /// <summary>
        /// 设备cpu
        /// </summary>
        public string EquipmentCPU { get; set; }

        /// <summary>
        /// 设备存储介质
        /// </summary>
        public string EquipmentDisk { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 变更标识
        /// </summary>
        public string ClientChangeFlag { get; set; }

       /// <summary>
       /// 创建时间
       /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 操作系统类型ID
        /// </summary>
        public int OsTypeID { get; set; }
        /// <summary>
        /// 设备类型ID
        /// </summary>
        public int EquipmentTypeID { get; set; }
        /// <summary>
        /// 点位名称
        /// </summary>
        public string StationName { get; set; }
        /// <summary>
        /// 点位描述
        /// </summary>
        public string StationDes { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>

        public string DepartmentName { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目类型ID
        /// </summary>
        public int ProjectTypeID { get; set; }
        /// <summary>
        /// 项目类型名称
        /// </summary>
        public string ProjectTypeName { get; set; }
        /// <summary>
        /// 设备IP地址
        /// </summary>
        public string EquipmentIP { get; set; }
   

    }
}