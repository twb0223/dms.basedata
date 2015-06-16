﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseData.Web.Areas.HelpPage.Models
{
    /// <summary>
    /// 设备参数类
    /// </summary>
    public class EqumentParameter : BaseData.Web.Areas.HelpPage.Parameter
    {
        /// <summary>
        /// 设备MAC地址
        /// </summary>
        public string EquipmentMac { get; set; }
    }
}