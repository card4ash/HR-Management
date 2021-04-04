using HrManagementAlpha.Models;
using HrManagementAlpha.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HrManagementAlpha.Controllers
{
    public class RolesController : Controller
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            var model = _dbContext.Roles.Select(r => new RoleViewModel { RoleId=r.Id,RoleName = r.Name }).ToList();
            return View(model);
        }

        // GET: Roles/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _dbContext.Roles.Where(r => r.Id == id).Select(r=>new RoleViewModel { RoleId = r.Id, RoleName = r.Name, EnRolledUser = r.Users.Select( eu=>new RoleUser {UserId=eu.UserId,UserName=_dbContext.Users.Where(urs=>urs.Id==eu.UserId).Select(n=>n.UserName).FirstOrDefault() } ) }).FirstOrDefault();
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "RoleName")] RoleViewModel role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                    var idResult = rm.Create(new IdentityRole(role.RoleName));
                    if (idResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(role);
                    }
                    
                }

                return View(role);

            }
            catch
            {
                return View(role);
            }
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _dbContext.Roles.Where(r => r.Id == id).Select(r => new RoleViewModel { RoleId = r.Id, RoleName = r.Name }).FirstOrDefault();
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "RoleId,RoleName")] RoleViewModel role)
        {
            try
            {
                var editRole=_dbContext.Roles.Where(r => r.Id == role.RoleId).FirstOrDefault();
                editRole.Name = role.RoleName;
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(role);
            }
        }

        // GET: Roles/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _dbContext.Roles.Where(r => r.Id == id).Select(r => new RoleViewModel { RoleId = r.Id, RoleName = r.Name }).FirstOrDefault();
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Roles/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(string id)
        {
            
            try
            {
                // TODO: Add delete logic here
                var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var appRoleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(_dbContext));
                var deleterole = appRoleManager.FindById(id);

                if (deleterole != null)
                {
                    IdentityResult result = appRoleManager.Delete(deleterole);

                    if (!result.Succeeded)
                    {
                        return HttpNotFound();
                    }

                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult RemoveUserFromRole(string roleid, string userid)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var appRoleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(_dbContext));
            var user = um.FindById(userid);
            var role = appRoleManager.FindById(roleid);
            um.RemoveFromRole(userid, role.Name);
            return Json(new { status = "success", msg = "" });
        }

        public ActionResult AssignRoleToUsers()
        {
            ViewBag.Roles = _dbContext.Roles.Select(r => new RoleCombo { ID = r.Id, Name = r.Name }).ToList();
            ViewBag.Departments = _dbContext.Departments.Select(d => new ComboItem { ID = d.DepartmentId, Name = d.DepartmentName }).ToList();
            return View();
        }
        public JsonResult getUsersInRole(string roleid, int departmentid)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var appRoleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(_dbContext));
            var role = appRoleManager.FindById(roleid);
            var empList = _dbContext.Employees.Where(e => e.DepartmentId == departmentid).Select(e => new { UserId = e.User.Id, UserName = e.User.UserName,Name=e.Person.PersonName }).ToList();
            List<EnRolledUser> result = new List<EnRolledUser>();
            foreach (var emp in empList)
            {
                EnRolledUser usr = new EnRolledUser();
                usr.UserName = emp.UserName;
                usr.UserId = emp.UserId;
                usr.Name = emp.Name;
                usr.IsInRole = um.IsInRole(emp.UserId, role.Name);
                result.Add(usr);
            }
            return Json(new { status="success",data=result});
        }
        public JsonResult UpdateUserRole(string roleid,string userid,bool statusid)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var appRoleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(_dbContext));
            var role = appRoleManager.FindById(roleid);
            if (statusid == true)
            {
                if (!um.IsInRole(userid, role.Name))
                {
                    um.AddToRole(userid, role.Name);
                }
            }
            else
            {
                if (um.IsInRole(userid, role.Name))
                {
                    um.RemoveFromRole(userid, role.Name);
                }
            }
            return Json(new { status = "success" });
        }
        public class EnRolledUser
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string Name { get; set; }
            public bool IsInRole { get; set; }
        }
        public class RoleCombo
        {
            public string ID { get; set; }
            public string Name { get; set; }
        }

    }
}
