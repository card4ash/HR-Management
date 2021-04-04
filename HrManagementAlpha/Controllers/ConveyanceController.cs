using HrManagementAlpha.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using HrManagementAlpha.Models;
using Microsoft.AspNet.Identity;

namespace HrManagementAlpha.Controllers
{
    [Authorize]
    public class ConveyanceController : Controller
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Conveynace
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateConveyance()
        {
            return View();
        }
        public ActionResult CreateConveyancePost(FormCollection fd)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                var empId = _dbContext.Employees.Where(e => e.UserId == userId).Select(e => e.EmployeeId).FirstOrDefault();
                string txtJobNo = fd["txtJobNo"].ToString();
                string txtCostCentre = fd["txtCostCentre"].ToString();
                string txtClientName = fd["txtClientName"].ToString();
                string isbillableToClient = fd["isbillableToClient"].ToString();
                string txttotalBill = fd["txttotalBill"].ToString();
                decimal TotalBill = 0.0m;
                if (!string.IsNullOrEmpty(txttotalBill))
                {
                    if (!decimal.TryParse(txttotalBill, out TotalBill))
                    {
                        return Json(new { status = "error", msg = "Total Bill Must be Number" });
                    }
                }
                bool IsBillable=false;
                if (!string.IsNullOrEmpty(isbillableToClient))
                {
                    if (!bool.TryParse(isbillableToClient, out IsBillable))
                    {
                        return Json(new { status = "error", msg = "Billable To Client Format Error!!" });
                    }
                }
                var rows = fd["rows"].ToString();
                var deserializedrows = serializer.Deserialize<List<ConveyanceBillRowVM>>(rows);
                var conveyancebill = new ConveyanceBill
                {
                    JobNo=txtJobNo,
                    CostCentre=txtCostCentre,
                    NameOfClient=txtClientName,
                    IsBillableToClient=IsBillable,
                    TotalBill=TotalBill,
                    SubmittedBy=empId,
                    IsApprovedByFinance=false,
                    IsApprovedByManager=false,
                    ConveyanceStatusId=1
                };
                _dbContext.ConveyanceBills.Add(conveyancebill);
                _dbContext.SaveChanges();
                foreach (var row in deserializedrows)
                {
                    var conveyancerow = new ConveyanceBillRow
                    {
                        ConveyanceBillId=conveyancebill.ConveynaceBillId,
                        RowDate=row.getRowDate,
                        Purpose=row.purpose,
                        FromLocation=row.fromLocation,
                        ToLocation=row.toLocaton,
                        MadeOfTransport=row.madeOfTransport,
                        Fare=row.fare
                    };
                    _dbContext.ConveyanceBillRows.Add(conveyancerow);
                }
                _dbContext.SaveChanges();
                return Json(new { status = "success" });
            }
            catch (Exception e)
            {
                return Json(new { status="error",msg="Parse Error"});
            }
            return Json("success");
        }
        public ActionResult SearchConveyance()
        {
            ViewBag.ConveyanceStatus = _dbContext.ConveyanceStatus.Select(c => new ComboItem { ID = c.ConveyanceStatusId, Name = c.ConveyanceStatusName }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult SearchConveyancePost(FormCollection fd)
        {
            var txtJobNo = fd["txtJobNo"].ToString();
            var txtCostCentre = fd["txtCostCentre"].ToString();
            var txtNameOfClient = fd["txtNameOfClient"].ToString();
            var txtLocation = fd["txtLocation"].ToString();
            var fromDate = fd["fromDate"].ToString();
            var toDate = fd["toDate"].ToString();
            
            DateTime tempDate;
            var conveyance = from a in _dbContext.ConveyanceBills select a;
            if (!string.IsNullOrEmpty(fromDate))
            {
                if (DateTime.TryParseExact(fromDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out tempDate))
                {
                    conveyance = from a in conveyance where a.SubmitDate >= tempDate select a;
                }
                {
                    return Json(new { status = "error", msg = "Wrong Date Format." });
                }
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                if (DateTime.TryParseExact(toDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out tempDate))
                {
                    conveyance = from a in conveyance where a.SubmitDate <= tempDate select a;
                }
                else
                {
                    return Json(new { status = "error", msg = "Wrong Date Format." });
                }
            }
            if (!string.IsNullOrEmpty(txtJobNo))
            {
                conveyance = from a in conveyance where a.JobNo.Contains(txtJobNo) select a;
            }
            if (!string.IsNullOrEmpty(txtCostCentre))
            {
                conveyance = from a in conveyance where a.CostCentre.Contains(txtCostCentre) select a;
            }
            if (!string.IsNullOrEmpty(txtNameOfClient))
            {
                conveyance = from a in conveyance where a.NameOfClient.Contains(txtNameOfClient) select a;
            }
            if (!string.IsNullOrEmpty(txtLocation))
            {
                conveyance = from a in conveyance where (a.ConveyanceBillRows.Any(c => c.FromLocation.Contains(txtLocation)) || a.ConveyanceBillRows.Any(c => c.ToLocation.Contains(txtLocation))) select a;
            }
            var conveyanceStatusid = fd["conveyanceStatusid"].ToString();
            int conveyancestatusid = 0;
            if (Int32.TryParse(conveyanceStatusid, out conveyancestatusid))
            {
                conveyance = from a in conveyance where a.ConveyanceStatusId == conveyancestatusid select a;
            }
            var result = (from a in conveyance select new
            {
                ConveyanceId=a.ConveynaceBillId,
                JobNo=a.JobNo,
                CostCentre=a.CostCentre,
                NameOfClient=a.NameOfClient,
                SubmitedBy=a.Employee.Person.PersonName,
                TotalRow=a.ConveyanceBillRows.Count,
                Total=a.TotalBill
            }).ToList();
            return Json(new { status = "success", data=result });
        }
        public ActionResult SearchConveyanceUser()
        {
            ViewBag.ConveyanceStatus = _dbContext.ConveyanceStatus.Select(c => new ComboItem { ID = c.ConveyanceStatusId, Name = c.ConveyanceStatusName }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult SearchConveyanceUserPost(FormCollection fd)
        {
            var txtJobNo = fd["txtJobNo"].ToString();
            var txtCostCentre = fd["txtCostCentre"].ToString();
            var txtNameOfClient = fd["txtNameOfClient"].ToString();
            var txtLocation = fd["txtLocation"].ToString();
            var fromDate = fd["fromDate"].ToString();
            var toDate = fd["toDate"].ToString();
            var conveyanceType = fd["conveyancetype"].ToString();
            int conveyancetypeid = 1;
            if (!Int32.TryParse(conveyanceType, out conveyancetypeid))
            {
                return Json(new { status = "success", msg = "conveyance type must be number" });
            }

            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var empId = _dbContext.Employees.Where(e => e.UserId == userId).Select(e => e.EmployeeId).FirstOrDefault();

            DateTime tempDate;
            var conveyance = from a in _dbContext.ConveyanceBills select a;
            if (conveyancetypeid == 1)
            {
                conveyance = from a in conveyance where a.SubmittedBy == empId select a;
            }
            else
            {
                conveyance = from a in conveyance where a.Employee.Boss.EmployeeId == empId select a;
            }
            if (!string.IsNullOrEmpty(fromDate))
            {
                if (DateTime.TryParseExact(fromDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out tempDate))
                {
                    conveyance = from a in conveyance where a.SubmitDate >= tempDate select a;
                }
                {
                    return Json(new { status = "error", msg = "Wrong Date Format." });
                }
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                if (DateTime.TryParseExact(toDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out tempDate))
                {
                    conveyance = from a in conveyance where a.SubmitDate <= tempDate select a;
                }
                else
                {
                    return Json(new { status = "error", msg = "Wrong Date Format." });
                }
            }
            if (!string.IsNullOrEmpty(txtJobNo))
            {
                conveyance = from a in conveyance where a.JobNo.Contains(txtJobNo) select a;
            }
            if (!string.IsNullOrEmpty(txtCostCentre))
            {
                conveyance = from a in conveyance where a.CostCentre.Contains(txtCostCentre) select a;
            }
            if (!string.IsNullOrEmpty(txtNameOfClient))
            {
                conveyance = from a in conveyance where a.NameOfClient.Contains(txtNameOfClient) select a;
            }
            if (!string.IsNullOrEmpty(txtLocation))
            {
                conveyance = from a in conveyance where (a.ConveyanceBillRows.Any(c => c.FromLocation.Contains(txtLocation)) || a.ConveyanceBillRows.Any(c => c.ToLocation.Contains(txtLocation))) select a;
            }
            var conveyanceStatusid = fd["conveyanceStatusid"].ToString();
            int conveyancestatusid = 0;
            if (Int32.TryParse(conveyanceStatusid, out conveyancestatusid))
            {
                conveyance = from a in conveyance where a.ConveyanceStatusId == conveyancestatusid select a;
            }
            var result = (from a in conveyance
                          select new
                          {
                              ConveyanceId = a.ConveynaceBillId,
                              JobNo = a.JobNo,
                              CostCentre = a.CostCentre,
                              NameOfClient = a.NameOfClient,
                              SubmitedBy = a.Employee.Person.PersonName,
                              TotalRow = a.ConveyanceBillRows.Count,
                              Total = a.TotalBill,
                              Status=a.ConveyanceStatu.ConveyanceStatusName
                          }).ToList();
            return Json(new { status = "success", data = result,type=conveyancetypeid });
        }
        public ActionResult viewConveyanceDetails(int id)
        {
            ViewBag.Conveyance = _dbContext.ConveyanceBills.Where(c => c.ConveynaceBillId == id).FirstOrDefault();
            return View();
            //return Json(new { status="success",data=data });
        }
        public ActionResult editConveyanceDetails(int id)
        {
            ViewBag.Conveyance = _dbContext.ConveyanceBills.Where(c => c.ConveynaceBillId == id).FirstOrDefault();
            return View();
        }
        public ActionResult ConveyanceDecline(int id)
        {
            var conveyance = _dbContext.ConveyanceBills.Where(c => c.ConveynaceBillId == id).FirstOrDefault();
            if(conveyance!=null)
            {
                conveyance.ConveyanceStatusId = 4;
                _dbContext.SaveChanges();
            }
            return Json(new { status = "success" });
        }
        public ActionResult ConveyanceApprove(int id)
        {
            var conveyance = _dbContext.ConveyanceBills.Where(c => c.ConveynaceBillId == id).FirstOrDefault();
            if (conveyance != null)
            {
                conveyance.ConveyanceStatusId = 2;
                _dbContext.SaveChanges();
            }
            return Json(new { status = "success" });
        }
        public ActionResult SearchConveyanceFinance()
        {
            ViewBag.ConveyanceStatus = _dbContext.ConveyanceStatus.Select(c => new ComboItem { ID = c.ConveyanceStatusId, Name = c.ConveyanceStatusName }).ToList();
            return View();
        }
        public ActionResult SearchConveyanceFinancePost(FormCollection fd)
        {
            var txtJobNo = fd["txtJobNo"].ToString();
            var txtCostCentre = fd["txtCostCentre"].ToString();
            var txtNameOfClient = fd["txtNameOfClient"].ToString();
            var txtLocation = fd["txtLocation"].ToString();
            var fromDate = fd["fromDate"].ToString();
            var toDate = fd["toDate"].ToString();
            DateTime tempDate;
            var conveyance = from a in _dbContext.ConveyanceBills select a;
            
            if (!string.IsNullOrEmpty(fromDate))
            {
                if (DateTime.TryParseExact(fromDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out tempDate))
                {
                    conveyance = from a in conveyance where a.SubmitDate >= tempDate select a;
                }
                {
                    return Json(new { status = "error", msg = "Wrong Date Format." });
                }
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                if (DateTime.TryParseExact(toDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out tempDate))
                {
                    conveyance = from a in conveyance where a.SubmitDate <= tempDate select a;
                }
                else
                {
                    return Json(new { status = "error", msg = "Wrong Date Format." });
                }
            }
            if (!string.IsNullOrEmpty(txtJobNo))
            {
                conveyance = from a in conveyance where a.JobNo.Contains(txtJobNo) select a;
            }
            if (!string.IsNullOrEmpty(txtCostCentre))
            {
                conveyance = from a in conveyance where a.CostCentre.Contains(txtCostCentre) select a;
            }
            if (!string.IsNullOrEmpty(txtNameOfClient))
            {
                conveyance = from a in conveyance where a.NameOfClient.Contains(txtNameOfClient) select a;
            }
            if (!string.IsNullOrEmpty(txtLocation))
            {
                conveyance = from a in conveyance where (a.ConveyanceBillRows.Any(c => c.FromLocation.Contains(txtLocation)) || a.ConveyanceBillRows.Any(c => c.ToLocation.Contains(txtLocation))) select a;
            }
            var conveyanceStatusid = fd["conveyanceStatusid"].ToString();
            int conveyancestatusid = 0;
            if (Int32.TryParse(conveyanceStatusid, out conveyancestatusid))
            {
                conveyance = from a in conveyance where a.ConveyanceStatusId == conveyancestatusid select a;
            }
            var result = (from a in conveyance
                          select new
                          {
                              ConveyanceId = a.ConveynaceBillId,
                              JobNo = a.JobNo,
                              CostCentre = a.CostCentre,
                              NameOfClient = a.NameOfClient,
                              SubmitedBy = a.Employee.Person.PersonName,
                              TotalRow = a.ConveyanceBillRows.Count,
                              Total = a.TotalBill,
                              Status = a.ConveyanceStatu.ConveyanceStatusName
                          }).ToList();
            return Json(new { status = "success", data = result });
        }
        public ActionResult editConveyanceDetailsFinance(int id)
        {
            ViewBag.Conveyance = _dbContext.ConveyanceBills.Where(c => c.ConveynaceBillId == id).FirstOrDefault();
            return View();
        }
        public ActionResult ConveyanceDeclineFinance(int id)
        {
            var conveyance = _dbContext.ConveyanceBills.Where(c => c.ConveynaceBillId == id).FirstOrDefault();
            if (conveyance != null)
            {
                conveyance.ConveyanceStatusId = 5;
                _dbContext.SaveChanges();
            }
            return Json(new { status = "success" });
        }
        public ActionResult ConveyanceApproveFinance(int id)
        {
            var conveyance = _dbContext.ConveyanceBills.Where(c => c.ConveynaceBillId == id).FirstOrDefault();
            if (conveyance != null)
            {
                conveyance.ConveyanceStatusId = 3;
                _dbContext.SaveChanges();
            }
            return Json(new { status = "success" });
        }
        public ActionResult ConveyanceCloseFinance(int id)
        {
            var conveyance = _dbContext.ConveyanceBills.Where(c => c.ConveynaceBillId == id).FirstOrDefault();
            if (conveyance != null)
            {
                conveyance.ConveyanceStatusId = 6;
                _dbContext.SaveChanges();
            }
            return Json(new { status = "success" });
        }

    }
}