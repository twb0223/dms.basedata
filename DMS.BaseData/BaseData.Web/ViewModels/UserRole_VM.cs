using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseData.Web.ViewModels
{
    public class UserRole_VM
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserAccount { get; set; }

        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public int PrjectID { get; set; }

        public string ProjectName { get; set; }

        public string Enable { get; set; }
    }
}