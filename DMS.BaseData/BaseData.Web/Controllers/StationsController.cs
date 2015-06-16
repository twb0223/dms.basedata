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
    public class StationsController : Controller
    {
        private MyDataContext db = new MyDataContext();
        public ActionResult Index(string key, int id = 1)
        {
            return ajaxSearchGetResult(key, id);
        }

        private ActionResult ajaxSearchGetResult(string key, int id = 1)
        {
            var qry = db.Stations.Include(x => x.Department).Include(x => x.Department.Project).AsQueryable();

            if (!String.IsNullOrWhiteSpace(key))
                qry = qry.Where(x => x.StationName.Contains(key) || x.Department.DepartmentName.Contains(key));
            var model = qry.OrderByDescending(a => a.StationID).ToPagedList(id, 10);
            if (Request.IsAjaxRequest())
                return PartialView("_StationSearchGet", model);
            return View(model);
        }

        // POST: Stations/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public async Task<ActionResult> Create(string jsonstr)
        {
            var res = new JsonResult();
            if (ModelState.IsValid)
            {
                var model = JsonConvert.DeserializeObject<Station>(jsonstr);
                model.StationID = "S" + DateTime.Now.ToString("yyyyMMddHHmmss");
                model.Status = 0;
                db.Stations.Add(model);
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
            Station model = db.Stations.Include(x=>x.Department).Include(x=>x.Department.Project).FirstOrDefault(x=>x.StationID==id);
            var res = new JsonResult();
            var vm = new
            {
                DepartmentID=model.DepartmentID,
                ProjectName = model.Department.Project.ProjectName,
                DepartmentName = model.Department.DepartmentName,
                StationName = model.StationName,
                StationDes = model.StationDes
            };
            res.Data = vm;
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string jsonstr)
        {
            var res = new JsonResult();
            if (ModelState.IsValid)
            {
                db.Entry(JsonConvert.DeserializeObject<Station>(jsonstr)).State = EntityState.Modified;
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
            Station sat = await db.Stations.FindAsync(id);
            var res = new JsonResult();
            if (sat == null)
            {
                res.Data = "ERROR";
            }
            else
            {
                //todo:删除点位关系，重置点位状态
                var EQSEntity = db.EquipmentStations.Where(x => x.StationID == id).FirstOrDefault();
                if (EQSEntity != null)
                {
                    var EqEntity = db.Equipments.Where(x => x.EquipmentID == EQSEntity.EquipmentID).FirstOrDefault();
                    if (EqEntity != null)
                    {
                        EqEntity.Status = 0;
                        db.Entry(EqEntity).State = EntityState.Modified;
                    }
                    db.Entry(EQSEntity).State = EntityState.Deleted;
                }

                db.Entry(sat).State = EntityState.Deleted;
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
