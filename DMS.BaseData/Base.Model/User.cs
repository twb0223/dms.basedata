﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseData.Model
{
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        /// <summary>
        /// 用户账号（登陆使用）
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string UserAccount { get; set; }

        /// <summary>
        /// 用户名称(显示用)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否可用 True:可用，false：禁用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [Required]
        public int DepartmentID { get; set; }

    
        /// <summary>
        /// 所属项目
        /// </summary>
        [Required]
        public int ProjectID { get; set; }
        

        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        public int RoleID { get; set; }
        ///// <summary>
        ///// 所属项目
        ///// </summary>
        //[Required]
        //public int DepartmentID { get; set; }

        //public virtual Department Project { get; set; }

    }
}
