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
    public class AuthorityMenusController : Controller
    {
        private MyDataContext db = new MyDataContext();

        // GET: AuthorityMenus
        public async Task<ActionResult> Index()
        {
            var authorityMenus = db.AuthorityMenus.Include(a => a.Project);
            return View(await authorityMenus.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Create(string jsonstr)
        {
            var res = new JsonResult();
            if (ModelState.IsValid)
            {
                //db.Entry(JsonConvert.DeserializeObject<Department>(jsonstr)).State = EntityState.Added;
                db.AuthorityMenus.Add(JsonConvert.DeserializeObject<AuthorityMenu>(jsonstr));
                await db.SaveChangesAsync();
                res.Data = "OK";
            }
            else
            {
                res.Data = "ERROR";
            }
            return res;
        }

        public async Task<ActionResult> GetForEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorityMenu model = await db.AuthorityMenus.FindAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            else
            {
                var res = new JsonResult();
                res.Data = model;
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
                db.Entry(JsonConvert.DeserializeObject<AuthorityMenu>(jsonstr)).State = EntityState.Modified;
                await db.SaveChangesAsync();
                res.Data = "OK";
            }
            else
            {
                res.Data = "ERROR";
            }
            return res;
        }


        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorityMenu model = await db.AuthorityMenus.FindAsync(id);
            var res = new JsonResult();
            if (model == null)
            {
                res.Data = "ERROR";
            }
            else
            {
                db.AuthorityMenus.Remove(model);
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
