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
using BaseData.Model;
using BaseData.DataAccess;

namespace BaseData.Web.Controllers
{
    /// <summary>
    /// 部门信息API
    /// </summary>
    public class DepartmentsAPIController : ApiController
    {
        private MyDataContext db = new MyDataContext();

        
        // GET: api/DepartmentsAPI
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns>返回部门集合</returns>
        public IQueryable<Department> GetDepartments()
        {
            return db.Departments;
        }
        // GET: api/DepartmentsAPI?ProjectID={ProjectID}
        /// <summary>
        /// 通过项目获取部门集合
        /// </summary>
        /// <param name="ProjectID">项目ID</param>
        /// <returns>返回部门集合</returns>
        public IQueryable<Department> GetDepartmentsByProjectID(int ProjectID)
        {
            return db.Departments.Include(x => x.Project).Where(x => x.ProjectID == ProjectID);
        }
        // GET: api/DepartmentsAPI/5
        /// <summary>
        /// 获取部门对象
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>返回部门对象</returns>
        [ResponseType(typeof(Department))]
        public async Task<IHttpActionResult> GetDepartment(int id)
        {
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool DepartmentExists(int id)
        {
            return db.Departments.Count(e => e.DepartmentID == id) > 0;
        }
    }
}