using HrManagementAlpha.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HrManagementAlpha.Controllers
{
    public class LeaveController : Controller
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Leave
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LeaveRequest()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var id = _dbContext.Employees.Where(e => e.UserId == userId).Select(e => e.EmployeeId).FirstOrDefault();
            var employee = _dbContext.Employees.Include("Person").Where(e => e.EmployeeId == id).FirstOrDefault();
            ViewBag.Employee = employee;
            var leavestat = _dbContext.LeaveStatistics.Where(e => e.EmployeeId == id).ToList();
            ViewBag.LeaveStat = leavestat;
            
            return View();
        }
    }
}