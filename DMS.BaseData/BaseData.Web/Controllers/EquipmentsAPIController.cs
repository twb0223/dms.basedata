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

namespace BaseData.Web.Controllers
{
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
            return db.Equements;
        }

        // GET: api/EquipmentsAPI/5
        /// <summary>
        /// 获取设备对象
        /// </summary>
        /// <param name="id">设备ID</param>
        /// <returns>设备对象</returns>
        [ResponseType(typeof(Equipment))]
        public async Task<IHttpActionResult> GetEquipment(string id)
        {
            Equipment equipment = await db.Equements.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            return Ok(equipment);
        }

        // PUT: api/EquipmentsAPI/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutEquipment(string id, Equipment equipment)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != equipment.EquipmentID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(equipment).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EquipmentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/EquipmentsAPI
        /// <summary>
        /// 添加设备，终端首次接入是调用
        /// </summary>
        /// <param name="equipment">设备对象</param>
        /// <returns>结果OK，Error</returns>
        [ResponseType(typeof(Equipment))]
        public async Task<IHttpActionResult> PostEquipment(Equipment equipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Equements.Add(equipment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EquipmentExists(equipment.EquipmentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
           // return Ok(equipment);
            //return CreatedAtRoute("DefaultApi", new { id = equipment.EquipmentID }, equipment);
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
            return db.Equements.Count(e => e.EquipmentID == id) > 0;
        }
    }
}