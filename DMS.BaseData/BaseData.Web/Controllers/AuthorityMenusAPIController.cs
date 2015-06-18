﻿using System;
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

namespace BaseData.Web.Controllers
{
    public class AuthorityMenusAPIController : ApiController
    {
        private MyDataContext db = new MyDataContext();


        // GET: api/AuthorityMenusAPI
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public IQueryable<AuthorityMenu> GetAuthorityMenus()
        {
            return db.AuthorityMenus;
        }

        // GET: api/AuthorityMenusAPI/5
        [ResponseType(typeof(AuthorityMenu))]
        public async Task<IHttpActionResult> GetAuthorityMenu(string id)
        {
            AuthorityMenu authorityMenu = await db.AuthorityMenus.FindAsync(id);
            if (authorityMenu == null)
            {
                return NotFound();
            }

            return Ok(authorityMenu);
        }

        public IQueryable<AuthorityMenu> GetAuthorityByProjectID(int ProjectID)
        {
            return db.AuthorityMenus.Include(x => x.Project).Where(x => x.ProjectID == ProjectID);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="ProjectID">项目ID</param>
        /// <param name="flag">是否分组</param>
        /// <returns></returns>
        [ResponseType(typeof(RoleAuthorityMenus))]
        public async Task<IHttpActionResult> GetAuthorityMenuByProjectID(int ProjectID, bool flag)
        {
            var amList = await db.AuthorityMenus.Include(x => x.Project).Where(x => x.ProjectID == ProjectID).ToListAsync();
            RoleAuthorityMenus ram = new RoleAuthorityMenus();
            List<ParentMenu> rpmlist = new List<ParentMenu>();
            var pmlist = amList.Where(x => x.ParentMenuCode == "");//父级菜单列表
            var cmlist = amList.Where(x => x.ParentMenuCode != "");//子级菜单

            foreach (var pm in pmlist)
            {
                ParentMenu p = new ParentMenu();
                p.MenuCode = pm.MenuCode;
                p.MenuName = pm.MenuName;
                p.URL = pm.URL;
                List<ChildMenu> rcmlist = new List<ChildMenu>();
                foreach (var cm in cmlist)
                {
                    if (cm.ParentMenuCode == pm.MenuCode)
                    {
                        ChildMenu c = new ChildMenu();
                        c.ChildMenuCode = cm.MenuCode;
                        c.ChildMenuName = cm.MenuName;
                        c.URL = cm.URL;
                        rcmlist.Add(c);
                    }
                }

                p.ChildMenus = rcmlist;
                rpmlist.Add(p);
            }
            ram.Menus = rpmlist;
            ram.ProjectID = ProjectID;
            return Json(ram);
        }

        //// PUT: api/AuthorityMenusAPI/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutAuthorityMenu(string id, AuthorityMenu authorityMenu)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != authorityMenu.MenuCode)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(authorityMenu).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AuthorityMenuExists(id))
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

        //// POST: api/AuthorityMenusAPI
        //[ResponseType(typeof(AuthorityMenu))]
        //public async Task<IHttpActionResult> PostAuthorityMenu(AuthorityMenu authorityMenu)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.AuthorityMenus.Add(authorityMenu);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (AuthorityMenuExists(authorityMenu.MenuCode))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = authorityMenu.MenuCode }, authorityMenu);
        //}

        //// DELETE: api/AuthorityMenusAPI/5
        //[ResponseType(typeof(AuthorityMenu))]
        //public async Task<IHttpActionResult> DeleteAuthorityMenu(string id)
        //{
        //    AuthorityMenu authorityMenu = await db.AuthorityMenus.FindAsync(id);
        //    if (authorityMenu == null)
        //    {
        //        return NotFound();
        //    }

        //    db.AuthorityMenus.Remove(authorityMenu);
        //    await db.SaveChangesAsync();

        //    return Ok(authorityMenu);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuthorityMenuExists(string id)
        {
            return db.AuthorityMenus.Count(e => e.MenuCode == id) > 0;
        }
    }
}