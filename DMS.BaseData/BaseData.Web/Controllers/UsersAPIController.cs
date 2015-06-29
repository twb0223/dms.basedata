using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BaseData.DataAccess;
using BaseData.Model;
using BaseData.Web.ViewModels;
using BaseData.Web.Common;
using Newtonsoft.Json;

namespace BaseData.Web.Controllers
{
    /// <summary>
    /// 用户信息API
    /// </summary>
    public class UsersAPIController : ApiController
    {
        private MyDataContext db = new MyDataContext();

        /// <summary>
        /// 登陆用户获取账号信息，权限菜单
        /// </summary>
        /// <param name="Account">账号</param>
        /// <param name="Password">密码(MD5加密)</param>
        /// <returns>返回用户信息及权限菜单</returns>
        [ResponseType(typeof(UserInfo))]
        public async Task<IHttpActionResult> GetCheckUser(string Account, string Password)
        {
            User vm = await db.Users.Where(x => x.UserAccount == Account && x.Password == Password).FirstOrDefaultAsync();

            UserInfo model = new UserInfo();

            if (vm == null)
            {
                var res = new
                {
                    Result = "None"
                };
                return Json(res);
            }
            else
            {
                if (!vm.Enable) //如果该账号被锁定
                {
                    var res = new
                    {
                        Result = "账号被锁定，请联系管理员！"
                    };
                    return Json(res);
                }
                model.UserAccount = vm.UserAccount;
                model.UserName = vm.UserName;
                model.RoleID = vm.RoleID;
                model.RoleName = db.Roles.Find(vm.RoleID).RoleName;
                model.ProjectID = vm.ProjectID;
                model.ProjectName = db.Projects.Find(vm.ProjectID).ProjectName;
                model.DepartmentID = vm.DepartmentID;
                model.DepartmentName = db.Departments.Find(vm.DepartmentID).DepartmentName;
                var ra=db.RoleAuthoritys.Where(x => x.RoleID == vm.RoleID).FirstOrDefault();
                if (ra != null)
                {
                    model.MenuJson = JsonConvert.DeserializeObject<RoleAuthorityMenus>(ra.MenuJson);
                }
                else
                {
                    var res = new
                    {
                        Result = "该用户对应角色没有配置权限菜单，请联系管理员！"
                    };
                    return Json(res);
                }
              
            }
            return Json(model);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserID == id) > 0;
        }
    }
}