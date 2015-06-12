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
using Base.Model;
using BaseData.DataAccess;

namespace BaseData.Web.Controllers
{
    public class ProjectsAPIController : ApiController
    {
        private MyDataContext db = new MyDataContext();

        // GET: api/ProjectsAPI
        public IQueryable<Project> GetProjects()
        {
            return db.Projects;
        }

        // GET: api/ProjectsAPI/5
        [ResponseType(typeof(Project))]
        public async Task<IHttpActionResult> GetProject(int id)
        {
            Project project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
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