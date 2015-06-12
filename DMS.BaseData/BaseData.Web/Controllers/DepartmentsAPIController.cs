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
using Base.Model;
using BaseData.DataAccess;

namespace BaseData.Web.Controllers
{
    public class DepartmentsAPIController : ApiController
    {
        private MyDataContext db = new MyDataContext();

 
        // GET: api/DepartmentsAPI
        public IQueryable<Department> GetDepartments()
        {
            return db.Departments;
        }
        // GET: api/DepartmentsAPI?ProjectID={ProjectID}
        public IQueryable<Department> GetDepartmentsByProjectID(int ProjectID)
        {
            return db.Departments.Include(x => x.Project).Where(x => x.ProjectID == ProjectID);
        }
        // GET: api/DepartmentsAPI/5
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