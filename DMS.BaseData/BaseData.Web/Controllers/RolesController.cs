using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaseData.DataAccess;
using BaseData.Model;
using Newtonsoft.Json;

namespace BaseData.Web.Controllers
{
    public class RolesController : Controller
    {
        private MyDataContext db = new MyDataContext();

        // GET: Roles
        public async Task<ActionResult> Index()
        {
            return View(await db.Roles.Include(x=>x.Project).ToListAsync());
        }

        // GET: Roles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = await db.Roles.FindAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        public async Task<ActionResult> Create(string jsonstr)
        {
            var res = new JsonResult();
            if (ModelState.IsValid)
            {
                //db.Entry(JsonConvert.DeserializeObject<Department>(jsonstr)).State = EntityState.Added;
                db.Roles.Add(JsonConvert.DeserializeObject<Role>(jsonstr));
                await db.SaveChangesAsync();
                res.Data = "OK";
            }
            else
            {
                res.Data = "ERROR";
            }
            return res;
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role model = await db.Roles.FindAsync(id);
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
                    RoleName = model.RoleName
                };
                res.Data = vm;
                res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return res;
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(string jsonstr)
        {
            var res = new JsonResult();
            if (ModelState.IsValid)
            {
                db.Entry(JsonConvert.DeserializeObject<Role>(jsonstr)).State = EntityState.Modified;
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
            Role model = await db.Roles.FindAsync(id);
            var res = new JsonResult();
            if (model == null)
            {
                res.Data = "ERROR";
            }
            else
            {
                db.Roles.Remove(model);
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
