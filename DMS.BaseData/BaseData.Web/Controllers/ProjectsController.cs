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

        // GET: Projects
        public async Task<ActionResult> Index()
        {
            return View(await db.Projects.ToListAsync());
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

        public JsonResult Create(string ProjectName)
        {
            Project project = new Project();
            project.ProjectName = ProjectName;
            var res = new JsonResult();
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                res.Data = "OK";
            }
            else
            {
                res.Data = "Error";
            }
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
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
                res.Data = project.ProjectName;
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
