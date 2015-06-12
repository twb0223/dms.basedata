using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Base.Model;
using BaseData.DataAccess;
using Newtonsoft.Json;

namespace BaseData.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private MyDataContext db = new MyDataContext();

        public ActionResult Index()
        {
            var list = db.Projects.Include(x => x.ProjectType).ToList();
            return View(list);
        }

        // GET: Projects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        public JsonResult GetProjectType()
        {
            var res = new JsonResult();
            res.Data = db.ProjectType.ToList();
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        }

        [HttpPost]
        public async Task<ActionResult> Create(string jsonstr)
        {
            var res = new JsonResult();
            if (ModelState.IsValid)
            {
                //db.Entry(JsonConvert.DeserializeObject<Department>(jsonstr)).State = EntityState.Added;
                db.Projects.Add(JsonConvert.DeserializeObject<Project>(jsonstr));
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
            Project project = await db.Projects.FindAsync(id);
            var res = new JsonResult();

            if (project == null)
            {
                return HttpNotFound();
            }
            else
            {
                var vm = new
                {
                    ProjectID = project.ProjectID,
                    ProjectName = project.ProjectName,
                    ProjectTypeID = project.ProjectTypeID
                };
                res.Data = vm;
            }
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        }

        // POST: Projects/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string project)
        {
            var res = new JsonResult();
            if (ModelState.IsValid)
            {
                db.Entry(JsonConvert.DeserializeObject<Project>(project)).State = EntityState.Modified;
                await db.SaveChangesAsync();
                res.Data = "OK";
            }
            else
            {
                res.Data = "ERROR";
            }
            return res;
        }

        // GET: Projects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await db.Projects.FindAsync(id);
            var res = new JsonResult();
            if (project == null)
            {
                res.Data = "ERROR";
            }
            else
            {
                db.Projects.Remove(project);
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
