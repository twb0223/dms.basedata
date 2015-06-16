using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaseData.Model;
using Webdiyer.WebControls.Mvc;
using BaseData.DataAccess;
using Webdiyer.WebControls;
using Newtonsoft.Json;

namespace BaseData.Web.Controllers
{
    public class EquipmentStationsController : Controller
    {
        private MyDataContext db = new MyDataContext();

        // GET: EquipmentStations
        public ActionResult Index(string key,int id=1)
        {
            return ajaxSearchGetResult(key, id);
        }
        private ActionResult ajaxSearchGetResult(string key, int id = 1)
        {
            var qry = db.EquipmentStations.Include(x=>x.Equipment).Include(x=>x.Station).Include(x=>x.Station.Department).Include(x=>x.Station.Department.Project).AsQueryable();
            if (!String.IsNullOrWhiteSpace(key))
                qry = qry.Where(x => x.Equipment.EquipmentName.Contains(key) || x.Station.StationName.Contains(key) || x.StationID.Contains(key) || x.EquipmentID.Contains(key));
            var model = qry.OrderByDescending(a => a.ID).ToPagedList(id, 10);
            if (Request.IsAjaxRequest())
                return PartialView("_EquipmentStationSearchGet", model);
            return View(model);
        }
        // GET: EquipmentStations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentStation equipmentStation = await db.EquipmentStations.FindAsync(id);
            if (equipmentStation == null)
            {
                return HttpNotFound();
            }
            return View(equipmentStation);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentStation dep = await db.EquipmentStations.FindAsync(id);
            var res = new JsonResult();
            if (dep == null)
            {
                res.Data = "ERROR";
            }
            else
            {
                db.EquipmentStations.Remove(dep);
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
