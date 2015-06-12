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
using Webdiyer.WebControls.Mvc;
using BaseData.DataAccess;
using Webdiyer.WebControls;
using Newtonsoft.Json;

namespace BaseData.Web.Controllers
{
    public class EquementsController : Controller
    {
        private MyDataContext db = new MyDataContext();

        public ActionResult Index(string key, int id = 1)
        {
            return ajaxSearchGetResult(key, id);
        }

        private ActionResult ajaxSearchGetResult(string key, int id = 1)
        {
            var qry = db.Equements.AsQueryable();
            if (!String.IsNullOrWhiteSpace(key))
                qry = qry.Where(x => x.EquipmentMac.Contains(key) || x.EquipmentName.Contains(key)||x.EquipmentID.Contains(key));
            var model = qry.OrderByDescending(a => a.CreateTime).ToPagedList(id, 10);
            if (Request.IsAjaxRequest())
                return PartialView("_EquipmentSearchGet", model);
            return View(model);
        }

        // GET: Equements/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equement equement = await db.Equements.FindAsync(id);
            if (equement == null)
            {
                return HttpNotFound();
            }
            return View(equement);
        }

        [HttpPost]
        public async Task<ActionResult> Create(string jsonstr)
        {
            var res = new JsonResult();
            if (ModelState.IsValid)
            {
                var model = JsonConvert.DeserializeObject<Equement>(jsonstr);
                model.Status = 0;
                model.CreateTime = DateTime.Now;
                model.CreateBy = "Client";
                model.ClientChangeFlag = Guid.NewGuid().ToString();
                db.Equements.Add(model);
                await db.SaveChangesAsync();
                res.Data = "OK";
            }
            else
            {
                res.Data = "ERROR";
            }
            return res;
        }
        public JsonResult GetForEdit(string id)
        {
            var res = new JsonResult();
            Equement model = db.Equements.FirstOrDefault(x => x.EquipmentID == id);
            res.Data = model;
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        }
        [HttpPost]
        public async Task<ActionResult> Edit(string jsonstr)
        {
            var res = new JsonResult();
            if (ModelState.IsValid)
            {
                var model = JsonConvert.DeserializeObject<Equement>(jsonstr);
                model.Status = 0;
                model.CreateTime = DateTime.Now;
                model.CreateBy = "Client";
                model.ClientChangeFlag = Guid.NewGuid().ToString();
                db.Entry(model).State = EntityState.Modified;
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
            Equement model = await db.Equements.FindAsync(id);
            var res = new JsonResult();
            if (model == null)
            {
                res.Data = "ERROR";
            }
            else
            {
                db.Equements.Remove(model);
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
