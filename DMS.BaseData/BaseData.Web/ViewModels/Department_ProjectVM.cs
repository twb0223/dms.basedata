using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseData.Web.ViewModels
{
    public class Department_ProjectVM
    {
        public int DepartmentID { get; set; }
        public int ProjectID { get; set; }

        public string DepartmentName { get; set; }

        public string ProjectName { get; set; }
    }
}