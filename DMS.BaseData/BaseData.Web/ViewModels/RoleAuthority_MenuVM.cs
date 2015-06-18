using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseData.Web.ViewModels
{
    public class RoleAuthorityMenus
    {
        //public int RoleID { get; set; }
        public int ProjectID { get; set; }
        public List<ParentMenu> Menus { get; set; }


    }
    public class ParentMenu
    {
        public string MenuCode { get; set; }
        public string MenuName { get; set; }

        public string URL { get; set; }

        public List<ChildMenu> ChildMenus { get; set; }
    }
    public class ChildMenu
    {
        public string ChildMenuCode { get; set; }
        public string ChildMenuName { get; set; }

        public string URL { get; set; }

        public bool Enable { get; set; }
    }
}