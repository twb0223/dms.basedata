using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Base.Model;
using BaseData.DataAccess;
using Newtonsoft.Json;
using BaseData.Web.ViewModels;

namespace BaseData.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private MyDataContext db = new MyDataContext();

        // GET: Departments
        public ActionResult Index()
        {
            var list = db.Departments.Include(x => x.Project).ToList();
            var vlist = new List<Department_ProjectVM>();
            list.ForEach(x =>
            {
                Department_ProjectVM vm = new Department_ProjectVM();
                vm.DepartmentID = x.DepartmentID;
                vm.DepartmentName = x.DepartmentName;
                vm.ProjectID = x.ProjectID;
                vm.ProjectName = x.Project.ProjectName;
                vlist.Add(vm);
            });

            return View(vlist);
        }

        // GET: Departments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        [HttpPost]
        public async Task<ActionResult> Create(string jsonstr)
        {
            var res = new JsonResult();
            if (ModelState.IsValid)
            {
                //db.Entry(JsonConvert.DeserializeObject<Department>(jsonstr)).State = EntityState.Added;
                db.Departments.Add(JsonConvert.DeserializeObject<Department>(jsonstr));
                await db.SaveChangesAsync();
                res.Data = "OK";
            }
            else
            {
                res.Data = "ERROR";
            }
            return res;
        }

        // GET: Projects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department model = await db.Departments.FindAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            else
            {
                var res = new JsonResult();
                var vm = new
                {
                    ProjectID = model.ProjectID,
                    DepartmentName = model.DepartmentName
                };
                res.Data = vm;
                res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return res;
            }
        }

        // POST: Projects/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string dep)
        {
            var res = new JsonResult();
            if (ModelState.IsValid)
            {
                db.Entry(JsonConvert.DeserializeObject<Department>(dep)).State = EntityState.Modified;
                await db.SaveChangesAsync();
                res.Data = "OK";
            }
            else
            {
                res.Data = "ERROR";
            }
            return res;
        }



        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department dep = await db.Departments.FindAsync(id);
            var res = new JsonResult();
            if (dep == null)
            {
                res.Data = "ERROR";
            }
            else
            {
                db.Departments.Remove(dep);
                await db.SaveChangesAsync();
                res.Data = "OK";
            }
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
