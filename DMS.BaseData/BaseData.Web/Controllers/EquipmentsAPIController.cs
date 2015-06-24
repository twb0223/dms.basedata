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
using BaseData.DataAccess;
using BaseData.Model;

using BaseData.Web.ViewModels;
using System.Data.SqlClient;

namespace BaseData.Web.Controllers
{
    /// <summary>
    /// 设备信息API
    /// </summary>
    public class EquipmentsAPIController : ApiController
    {
        private MyDataContext db = new MyDataContext();

        // GET: api/EquipmentsAPI
        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns>返回设备集合</returns>
        public IQueryable<Equipment> GetEquements()
        {
            return db.Equipments;
        }

        // GET: api/EquipmentsAPI/5
        /// <summary>
        /// 获取设备对象,
        /// </summary>
        /// <param name="id">设备ID</param>
        /// <returns>设备对象</returns>
        [ResponseType(typeof(Equipment))]
        public async Task<IHttpActionResult> GetEquipment(string id)
        {
            Equipment equipment = await db.Equipments.FindAsync(id);
            if (equipment == null)
            {
                var vm = new
                {
                    Result = "None"
                };
                return Json(vm);
            }

            return Ok(equipment);
        }
        /// <summary>
        /// 获取设备点位详细信息
        /// </summary>
        /// <param name="EquipmentID">设备ID</param>
        /// <returns></returns>
        [ResponseType(typeof(EquipmentStations_VM))]
        public async Task<IHttpActionResult> GetEquipmentStations(string EquipmentID)
        {
            string strsql = "Select * from [View_EquipmentStationDetail] where EquipmentID=@EquipmentID";
            var model = await db.Database.SqlQuery<EquipmentStations_VM>(strsql, new SqlParameter("@EquipmentID", EquipmentID)).FirstOrDefaultAsync();
            if (model == null)
            {
                var vm = new
                {
                    Result = "None"
                };
                return Json(vm);
            }
            return Ok(model);
        }
        /// <summary>
        /// 获取指定项目下所有设备点位信息
        /// </summary>
        /// <param name="ProjectID">项目ID</param>
        /// <returns>返回设备点位集合</returns>
        public IQueryable<EquipmentStations_VM> GetEquipmentStationsListByProject(int ProjectID)
        {
            string strsql = "Select * from [View_EquipmentStationDetail] where ProjectID=@ProjectID";
            return db.Database.SqlQuery<EquipmentStations_VM>(strsql, new SqlParameter("@ProjectID", ProjectID)).AsQueryable();
        }

        /// <summary>
        /// 获取指定部门下所有设备点位信息
        /// </summary>
        /// <param name="ProjectID">部门ID</param>
        /// <returns>返回设备点位集合</returns>
        public IQueryable<EquipmentStations_VM> GetEquipmentStationsListByDepartmentID(int DepartmentID)
        {
            string strsql = "Select * from [View_EquipmentStationDetail] where DepartmentID=@DepartmentID";
            return db.Database.SqlQuery<EquipmentStations_VM>(strsql, new SqlParameter("@DepartmentID", DepartmentID)).AsQueryable();
        }

        /// <summary>
        /// 通过Mac地址获取设备信息(Mac地址唯一)
        /// </summary>
        /// <param name="EquipmentMac">设备MAC地址</param>
        /// <returns>返回设备信息</returns>
        [ResponseType(typeof(EquipmentStations_VM))]
        public async Task<IHttpActionResult> GetEquipmentStationsListByProject(string EquipmentMac)
        {
            string strsql = "Select * from [View_EquipmentStationDetail] where EquipmentMac=@EquipmentMac";
            var model = await db.Database.SqlQuery<EquipmentStations_VM>(strsql, new SqlParameter("@EquipmentMac", EquipmentMac)).FirstOrDefaultAsync();
            if (model == null)
            {
                var vm = new
                {
                    Result = "None"
                };
                return Json(vm);
            }
            return Ok(model);
        }

        // POST: api/EquipmentsAPI
        /// <summary>
        /// 添加设备，终端首次接入是调用
        /// </summary>
        /// <param name="para">设备对象</param>
        /// <returns>结果OK，Error</returns>
        [ResponseType(typeof(Equipment))]
        public async Task<IHttpActionResult> PostEquipment(EquipmentParameter para)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.Equipments.Count(x=>x.EquipmentMac==para.EquipmentMac)>0)
            {
                var vm = new
                {
                    Result = "该设备Mac地址已注册，请检查！"
                };
                return Json(vm);
            }
            var entity = new Equipment();
            entity.EquipmentID = "OVI" + DateTime.Now.ToString("yyyyMMddhhmmss");
            entity.EquipmentMac = para.EquipmentMac;
            entity.EquipmentName = entity.EquipmentID;
            entity.OsTypeID = -1;
            entity.EquipmentTypeID = -1;
            entity.Status = 0;
            entity.CreateTime = DateTime.Now;
            entity.CreateBy = "Client";
            entity.ClientChangeFlag = Guid.NewGuid().ToString();

            db.Equipments.Add(entity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EquipmentExists(entity.EquipmentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = entity.EquipmentID }, entity);
        }

        //// DELETE: api/EquipmentsAPI/5
        //[ResponseType(typeof(Equipment))]
        //public async Task<IHttpActionResult> DeleteEquipment(string id)
        //{
        //    Equipment equipment = await db.Equements.FindAsync(id);
        //    if (equipment == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Equements.Remove(equipment);
        //    await db.SaveChangesAsync();

        //    return Ok(equipment);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipmentExists(string id)
        {
            return db.Equipments.Count(e => e.EquipmentID == id) > 0;
        }
    }
}