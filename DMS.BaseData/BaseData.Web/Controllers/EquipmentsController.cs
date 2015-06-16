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
    public class EquipmentsController : Controller
    {
        private MyDataContext db = new MyDataContext();

        public ActionResult Index(string key, string isUse, int id = 1)
        {
            return ajaxSearchGetResult(key, isUse, id);
        }

        private ActionResult ajaxSearchGetResult(string key, string isUse, int id = 1)
        {
            var qry = db.Equipments.AsQueryable();
            if (!String.IsNullOrWhiteSpace(key))
                qry = qry.Where(x => x.EquipmentMac.Contains(key) || x.EquipmentName.Contains(key) || x.EquipmentID.Contains(key));
            var model = qry.OrderByDescending(a => a.CreateTime).ToPagedList(id, 10);
            if (Request.IsAjaxRequest())
                return PartialView("_EquipmentSearchGet", model);
            return View(model);
        }

        public JsonResult GetForSetStations(string id)
        {
            var res = new JsonResult();

            var entity = db.EquipmentStations.Include(x => x.Station).Include(x => x.Station.Department).Include(x => x.Station.Department.Project).Where(x => x.EquipmentID == id).FirstOrDefault();
            if (entity != null)
            {
                var vm = new
                {
                    ID = entity.ID,
                    StationID = entity.StationID,
                    EquipmentID = entity.EquipmentID,
                    //EquipmentName = entity.Equipment.EquipmentName,
                    StationName = entity.Station.StationName,
                    DepartmentName = entity.Station.Department.DepartmentName,
                    ProjectName = entity.Station.Department.Project.ProjectName
                };
                res.Data = vm;
            }
            else
            {
                res.Data = "none";
            }
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        }

        [HttpPost]
        public async Task<ActionResult> SetStations(string jsonstr, string oldstationid)
        {
            var res = new JsonResult();
            var model = JsonConvert.DeserializeObject<EquipmentStation>(jsonstr);
            if (model.ID != 0)
            {
                db.Entry(model).State = EntityState.Modified;
            }
            else
            {
                db.Entry(model).State = EntityState.Added;
            }
            //todo：修改设备 状态，点位状态
            var eqentity = db.Equipments.Find(model.EquipmentID);
            eqentity.Status = 1;
            db.Entry(eqentity).State = EntityState.Modified;

            //修改新点位状态
            var stentity = db.Stations.Find(model.StationID);
            stentity.Status = 1;
            db.Entry(stentity).State = EntityState.Modified;
            //如果是修改
            if (model.StationID != oldstationid && oldstationid != "")
            {
                //修改旧点位状态
                var oldstentity = db.Stations.Find(oldstationid);
                oldstentity.Status = 0;
                db.Entry(oldstentity).State = EntityState.Modified;
            }
            try
            {
                await db.SaveChangesAsync();
                res.Data = "OK";
            }
            catch (Exception)
            {
                res.Data = "Error";
            }
            return res;
        }
        // GET: Equements/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equement = await db.Equipments.FindAsync(id);
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
                var model = JsonConvert.DeserializeObject<Equipment>(jsonstr);
                model.Status = 0;
                model.CreateTime = DateTime.Now;
                model.CreateBy = "Client";
                model.ClientChangeFlag = Guid.NewGuid().ToString();
                db.Equipments.Add(model);
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
            Equipment model = db.Equipments.FirstOrDefault(x => x.EquipmentID == id);
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
                var model = JsonConvert.DeserializeObject<Equipment>(jsonstr);
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
            Equipment model = await db.Equipments.FindAsync(id);
            var res = new JsonResult();
            if (model == null)
            {
                res.Data = "ERROR";
            }
            else
            {

                //todo:删除点位关系，重置点位状态
                var EQEntity = db.EquipmentStations.Where(x => x.EquipmentID == id).FirstOrDefault();
                if (EQEntity != null)
                {
                    var StaEntity = db.Stations.Where(x => x.StationID == EQEntity.StationID).FirstOrDefault();
                    if (StaEntity != null)
                    {
                        StaEntity.Status = 0;
                        db.Entry(StaEntity).State = EntityState.Modified;
                    }
                    db.Entry(EQEntity).State = EntityState.Deleted;
                }
                db.Entry(model).State = EntityState.Deleted;
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
