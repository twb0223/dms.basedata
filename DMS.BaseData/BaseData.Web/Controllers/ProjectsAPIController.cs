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
    /// 项目信息API
    /// </summary>
    public class ProjectsAPIController : ApiController
    {
        private MyDataContext db = new MyDataContext();

        // GET: api/ProjectsAPI
        /// <summary>
        /// 获取项目集合
        /// </summary>
        /// <returns>返回项目集合</returns>
        public IQueryable<Project> GetProjects()
        {
            return db.Projects;
        }

        // GET: api/ProjectsAPI/5
        /// <summary>
        /// 获取部门对象
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>返回部门对象</returns>
        [ResponseType(typeof(Project))]
        public async Task<IHttpActionResult> GetProject(int id)
        {
            Project project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                var vm = new
                {
                    Result = "None"
                };
                return Json(vm);
            }

            return Ok(project);
        }

        //// PUT: api/ProjectsAPI/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutProject(int id, Project project)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != project.ProjectID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(project).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProjectExists(id))
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

        //// POST: api/ProjectsAPI
        //[ResponseType(typeof(Project))]
        //public async Task<IHttpActionResult> PostProject(Project project)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Projects.Add(project);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = project.ProjectID }, project);
        //}

        //// DELETE: api/ProjectsAPI/5
        //[ResponseType(typeof(Project))]
        //public async Task<IHttpActionResult> DeleteProject(int id)
        //{
        //    Project project = await db.Projects.FindAsync(id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Projects.Remove(project);
        //    await db.SaveChangesAsync();

        //    return Ok(project);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.ProjectID == id) > 0;
        }
    }
}