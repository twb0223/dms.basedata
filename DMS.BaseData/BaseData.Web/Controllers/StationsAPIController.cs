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
using BaseData.Model;
using BaseData.DataAccess;

namespace BaseData.Web.Controllers
{
    /// <summary>
    /// 点位信息API
    /// </summary>
    public class StationsAPIController : ApiController
    {
        private MyDataContext db = new MyDataContext();

        // GET: api/StationsAPI
        /// <summary>
        /// 获取点位列表
        /// </summary>
        /// <returns>返回点位集合</returns>
        public IQueryable<Station> GetStations()
        {
            return db.Stations;
        }
      
        /// <summary>
        ///通过部门编号获取点位集合 
        /// </summary>
        /// <param name="DepartmentID">部门ID</param>
        /// <returns>返回该部门下所有点位</returns>
        public IQueryable<Station> GetStationsByDepmentID(int DepartmentID)
        {
            return db.Stations.Where(x => x.DepartmentID == DepartmentID);
        }

        /// <summary>
        /// 通过项目编号获取所有点位列表
        /// </summary>
        /// <param name="ProjectID">项目ID</param>
        /// <returns>返回该项目下所有点位信息</returns>
        public IQueryable<Station> GetStationsByProjectID(int ProjectID)
        {
            return db.Stations.Include(x => x.Department).Where(x => x.Department.ProjectID == ProjectID);
        }


        // GET: api/StationsAPI/5
        /// <summary>
        /// 通过点位ID获取点位
        /// </summary>
        /// <param name="id">点位ID</param>
        /// <returns>点位实体</returns>
        [ResponseType(typeof(Station))]
        public async Task<IHttpActionResult> GetStation(string id)
        {
            Station station = await db.Stations.FindAsync(id);
            if (station == null)
            {
                var vm = new
                {
                    Result = "None"
                };
                return Json(vm);
            }
            return Ok(station);
        }

        //// PUT: api/StationsAPI/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutStation(string id, Station station)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != station.StationID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(station).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StationExists(id))
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

        //// POST: api/StationsAPI
        //[ResponseType(typeof(Station))]
        //public async Task<IHttpActionResult> PostStation(Station station)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Stations.Add(station);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (StationExists(station.StationID))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = station.StationID }, station);
        //}

        //// DELETE: api/StationsAPI/5
        //[ResponseType(typeof(Station))]
        //public async Task<IHttpActionResult> DeleteStation(string id)
        //{
        //    Station station = await db.Stations.FindAsync(id);
        //    if (station == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Stations.Remove(station);
        //    await db.SaveChangesAsync();

        //    return Ok(station);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StationExists(string id)
        {
            return db.Stations.Count(e => e.StationID == id) > 0;
        }
    }
}