using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseData.Web.ViewModels
{
    public class UserParameter : BaseData.Web.Areas.HelpPage.Parameter
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password;
    }
}