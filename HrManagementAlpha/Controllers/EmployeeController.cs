using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HrManagementAlpha.Models;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HrManagementAlpha.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: Employee
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SearchEmployee()
        {
            ViewBag.Departments= _dbContext.Departments.Select(m => new ComboItem { ID = m.DepartmentId, Name = m.DepartmentName }).ToList();
            ViewBag.Designations = _dbContext.Designations.Select(d => new ComboItem { ID = d.DesignationId, Name = d.DesignationName }).ToList();
            ViewBag.Districts = _dbContext.Districts.Select(d => new ComboItem { ID = d.DISTRICT_ID, Name = d.DISTRICT_NAME }).ToList();
            ViewBag.MaritalStatus = _dbContext.MaritalStatuss.Select(m => new ComboItem { ID = m.MaritalStatusId, Name = m.MaritalStatusName }).ToList();
            ViewBag.Genders = _dbContext.Genders.Select(g => new ComboItem { ID = g.GenderId, Name = g.GenderName }).ToList();
            ViewBag.EducationLebel = _dbContext.EducationLevels.Select(e => new ComboItem { ID = e.EducationLevelId, Name = e.EducationLevelName }).ToList();
            return View();
        }
        [HttpPost]
        public JsonResult SearchEmployee(FormCollection fd)
        {
            string name= fd["txtName"].ToString();
            string departmentid= fd["departmentid"].ToString();
            string designationid = fd["designationid"].ToString();
            string email = fd["txtEmail"].ToString();
            string phoneno = fd["txtMobile"].ToString();
            string empcode = fd["txtEmpCode"].ToString();
            string nid = fd["txtNID"].ToString();
            string districtid = fd["districtid"].ToString();
            string thanaid = fd["thanaid"].ToString();
            string educationlebel = fd["educationlblid"].ToString();
            string genderid = fd["genderid"].ToString();
            string meritalstatusid = fd["maritalstatusid"].ToString();
            var data = _dbContext.Employees.Where(e => e.Active == true).Select(e => new
            {
                Id=e.EmployeeId,
                Name = e.Person.PersonName,
                Email = e.User.Email,
                Phone = e.Person.ContactNo,
                Department = e.Department.DepartmentName,
                Designation = e.Designation.DesignationName,
                Photo =String.IsNullOrEmpty(e.Person.ProfilePhoto)?"Default.jpg":e.Person.ProfilePhoto
            }).ToList();
            return Json(new {msg="success",data=data });
        }

        public ActionResult MyProfile()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var id = _dbContext.Employees.Where(e => e.UserId == userId).Select(e => e.EmployeeId).FirstOrDefault();
            var employee = _dbContext.Employees.Include("Person").Where(e => e.EmployeeId == id).FirstOrDefault();
            ViewBag.Employee = employee;
            ViewBag.IDCardTypes = _dbContext.IDCardTypes.Select(i => new ComboItem { ID = i.IDCardTypeId, Name = i.IDCardTypeName }).ToList();
            ViewBag.MaritalStatuses = _dbContext.MaritalStatuss.Select(m => new ComboItem { ID = m.MaritalStatusId, Name = m.MaritalStatusName }).ToList();
            ViewBag.Religions = _dbContext.Religions.Select(r => new ComboItem { ID = r.ReligionId, Name = r.ReligionName }).ToList();
            ViewBag.Genders = _dbContext.Genders.Select(g => new ComboItem { ID = g.GenderId, Name = g.GenderName }).ToList();
            ViewBag.EducationLebels = _dbContext.EducationLevels.Select(el => new ComboItem { ID = el.EducationLevelId, Name = el.EducationLevelName }).ToList();
            ViewBag.Results = _dbContext.Results.Select(r => new ComboItem { ID = r.ResultId, Name = r.ResultName }).ToList();
            ViewBag.Departments = _dbContext.Departments.Select(d => new ComboItem { ID = d.DepartmentId, Name = d.DepartmentName }).ToList();
            ViewBag.Designations = _dbContext.Designations.Select(d => new ComboItem { ID = d.DesignationId, Name = d.DesignationName }).ToList();
            ViewBag.Districts = _dbContext.Districts.Select(d => new ComboItem { ID = d.DISTRICT_ID, Name = d.DISTRICT_NAME }).ToList();
            if (employee != null)
            {
                if (employee.Person.DistrictId != null)
                {
                    ViewBag.Thanas = _dbContext.Thanas.Where(t => t.DISTRICT_ID == employee.Person.DistrictId).Select(t => new ComboItem { ID = t.THANA_ID, Name = t.THANA_NAME }).ToList();
                }
                if (employee.Person.PresentDistrictId != null)
                {
                    ViewBag.PThanas = _dbContext.Thanas.Where(t => t.DISTRICT_ID == employee.Person.PresentDistrictId).Select(t => new ComboItem { ID = t.THANA_ID, Name = t.THANA_NAME }).ToList();
                }
            }
            if (employee.Boss != null)
            {
                ViewBag.Bosses = _dbContext.Employees.Where(e => e.DepartmentId == employee.Boss.DepartmentId).Select(e => new ComboItem { ID = e.EmployeeId, Name = e.Person.PersonName + " (" + e.EmployeeCode + ")" }).ToList();
            }
            return View();
        }

        public ActionResult editEmployeeDetails(int id)
        {
            var employee = _dbContext.Employees.Include("Person").Where(e => e.EmployeeId == id).FirstOrDefault();
            ViewBag.Employee = employee;
            ViewBag.IDCardTypes = _dbContext.IDCardTypes.Select(i => new ComboItem { ID = i.IDCardTypeId, Name = i.IDCardTypeName }).ToList();
            ViewBag.MaritalStatuses = _dbContext.MaritalStatuss.Select(m => new ComboItem { ID = m.MaritalStatusId, Name = m.MaritalStatusName }).ToList();
            ViewBag.Religions = _dbContext.Religions.Select(r => new ComboItem { ID = r.ReligionId, Name = r.ReligionName }).ToList();
            ViewBag.Genders = _dbContext.Genders.Select(g => new ComboItem { ID = g.GenderId, Name = g.GenderName }).ToList();
            ViewBag.EducationLebels = _dbContext.EducationLevels.Select(el => new ComboItem { ID = el.EducationLevelId, Name = el.EducationLevelName }).ToList();
            ViewBag.Results = _dbContext.Results.Select(r => new ComboItem { ID = r.ResultId, Name = r.ResultName }).ToList();
            ViewBag.Departments = _dbContext.Departments.Select(d => new ComboItem { ID = d.DepartmentId, Name = d.DepartmentName }).ToList();
            ViewBag.Designations = _dbContext.Designations.Select(d => new ComboItem { ID = d.DesignationId, Name = d.DesignationName }).ToList();
            ViewBag.Offices = _dbContext.Offices.Select(d => new ComboItem { ID = d.OfficeId, Name = d.OfficeName }).ToList();
            ViewBag.Projects = _dbContext.Projects.Select(p => new ComboItem { ID = p.ProjectId, Name = p.ProjectName }).ToList();
            ViewBag.Districts = _dbContext.Districts.Select(d => new ComboItem { ID = d.DISTRICT_ID, Name = d.DISTRICT_NAME }).ToList();
            if (employee != null)
            {
                if (employee.Person.DistrictId != null)
                {
                    ViewBag.Thanas = _dbContext.Thanas.Where(t => t.DISTRICT_ID == employee.Person.DistrictId).Select(t => new ComboItem { ID = t.THANA_ID, Name = t.THANA_NAME }).ToList();
                }
                if (employee.Person.PresentDistrictId != null)
                {
                    ViewBag.PThanas = _dbContext.Thanas.Where(t => t.DISTRICT_ID == employee.Person.PresentDistrictId).Select(t => new ComboItem { ID = t.THANA_ID, Name = t.THANA_NAME }).ToList();
                }
            }

            if (employee.Boss != null)
            {
                ViewBag.Bosses = _dbContext.Employees.Where(e => e.DepartmentId == employee.Boss.DepartmentId).Select(e => new ComboItem { ID = e.EmployeeId, Name = e.Person.PersonName + " (" + e.EmployeeCode + ")" }).ToList();
            }
            return View();
        }
        [HttpPost]
        public ActionResult updateContactInfo(FormCollection fd)
        {
            int empId = Convert.ToInt32(fd["employeeid"].ToString());
            string name = fd["txtName"].ToString();
            string fatherName = fd["txtFatherName"].ToString();
            string motherName = fd["txtMotherName"].ToString();
            string email = fd["txtEmail"].ToString();
            string contactNo = fd["txtContactNo"].ToString();
            string secondContactNo = fd["txtSecondContact"].ToString();
            var employee = _dbContext.Employees.Where(e => e.EmployeeId == empId).FirstOrDefault();
            
            if (!employee.User.Email.Equals(email))
            {
                var chkemail = _dbContext.Users.Where(u => u.Email == email).FirstOrDefault();
                if (chkemail == null)
                {
                    employee.User.Email = email;
                    employee.User.UserName = email;
                }
                else
                {
                    return Json(new { status="error",msg="Email already exist!!!!" });
                }
            }
            employee.Person.PersonName = name;
            employee.Person.FatherName = fatherName;
            employee.Person.MotherName = motherName;
            employee.Person.ContactNo = contactNo;
            employee.Person.SecondContactNo = secondContactNo;
            _dbContext.SaveChanges();
            return Json(new { status="success"});
        }
        [HttpPost]
        public ActionResult updateMoreInfo(FormCollection fd)
        {
            int empId = 0;
            if (!Int32.TryParse(fd["employeeid4more"].ToString().Trim(),out empId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = _dbContext.Employees.Where(e => e.EmployeeId == empId).FirstOrDefault();
            if (employee != null)
            {
                string sgenderId = fd["genderid"].ToString().Trim();
                int genderid = 0;
                if (!string.IsNullOrEmpty(sgenderId))
                {
                    if (!Int32.TryParse(sgenderId, out genderid))
                    {
                        return Json(new { status = "error", msg = "Gender is Required" });//genderid carry garbage value
                    }
                    else
                    {
                        employee.Person.GenderId = genderid;
                    }
                }

                string dateofbirth = fd["txtBirthday"].ToString().Trim();
                DateTime DateOfBirth = DateTime.Now;
                if (!string.IsNullOrEmpty(dateofbirth))
                {
                    if (!DateTime.TryParseExact(dateofbirth, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateOfBirth))
                    {
                        return Json(new { status = "error", msg = "Invalid Birthday" });//carry invalid format value
                    }
                    else
                    {
                        employee.Person.DateOfBirth = DateOfBirth;
                    }
                }
                string sidcardtype = fd["idtypeid"].ToString().Trim();
                int idcardtype = 0;
                if (!string.IsNullOrEmpty(sidcardtype))
                {
                    if (!Int32.TryParse(sidcardtype, out idcardtype))
                    {
                        return Json(new { status = "error", msg = "ID Card Type Invalid" });//carry garbage value
                    }
                    else
                    {
                        employee.Person.IDCardTypeId = idcardtype;
                    }
                }
                string idcardno = fd["txtIDCardNo"].ToString().Trim();
                if (!string.IsNullOrEmpty(idcardno))
                {
                    employee.Person.IDCardNo = idcardno;
                }
                string smaritalstatusid = fd["maritalid"].ToString().Trim();
                int maritalstatusid = 0;
                if (!string.IsNullOrEmpty(smaritalstatusid))
                {
                    if (!Int32.TryParse(smaritalstatusid, out maritalstatusid))
                    {
                        return Json(new { status = "error", msg = "Marital Status Invalid" });//carry garbage value
                    }
                    else
                    {
                        employee.Person.MaritalStatusId = maritalstatusid;
                    }
                }
                string sreligionid = fd["religionid"].ToString().Trim();
                int religionid = 0;
                if (!string.IsNullOrEmpty(sreligionid))
                {
                    if (!Int32.TryParse(sreligionid, out religionid))
                    {
                        return Json(new { status = "error", msg = "Invalid Religion." });//carry garbage value
                    }
                    else
                    {
                        employee.Person.ReligionId = religionid;
                    }
                }
                string nationality = fd["txtNationality"].ToString().Trim();
                if (!string.IsNullOrEmpty(nationality))
                {
                    employee.Person.Nationality = nationality;
                }
                _dbContext.SaveChanges();
                return Json(new { status = "success" });
            }
            else
            {
                return Json(new { status = "error",msg="Employee not Found!" });
            }
        }
        [HttpPost]
        public ActionResult updateEmployeeInfo(FormCollection fd)
        {
            int empId = 0;
            if (!Int32.TryParse(fd["employeeid4empinfo"].ToString().Trim(), out empId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = _dbContext.Employees.Where(e => e.EmployeeId == empId).FirstOrDefault();

            string empCode = fd["txtEmpCode"].ToString().Trim();
            if (!string.IsNullOrEmpty(empCode))
            {
                employee.EmployeeCode = empCode;
            }
            string sofficeId = fd["pdepartmentid"].ToString().Trim();
            int officeId = 0;
            if (!string.IsNullOrEmpty(sofficeId))
            {
                if (!Int32.TryParse(sofficeId, out officeId))
                {
                    return Json(new { status = "error", msg = "Office Invalid" });//carry garbage value
                }
                else
                {
                    employee.OfficeId = officeId;
                }
            }
            string sprojectId = fd["pdepartmentid"].ToString().Trim();
            int projectid = 0;
            if (!string.IsNullOrEmpty(sprojectId))
            {
                if (!Int32.TryParse(sprojectId, out projectid))
                {
                    return Json(new { status = "error", msg = "Project Invalid" });//carry garbage value
                }
                else
                {
                    employee.ProjectId = projectid;
                }
            }
            string sdepartmentId = fd["pdepartmentid"].ToString().Trim();
            int departmentId = 0;
            if (!string.IsNullOrEmpty(sdepartmentId))
            {
                if (!Int32.TryParse(sdepartmentId, out departmentId))
                {
                    return Json(new { status = "error", msg = "Department Invalid" });//carry garbage value
                }
                else
                {
                    employee.DepartmentId = departmentId;
                }
            }
            string sdesignationId = fd["pdesignationid"].ToString().Trim();
            int designationId = 0;
            if (!string.IsNullOrEmpty(sdesignationId))
            {
                if (!Int32.TryParse(sdesignationId, out designationId))
                {
                    return Json(new { status = "error", msg = "Designation Invalid" });//carry garbage value
                }
                else
                {
                    employee.DesignationId = designationId;
                }
            }
            string sbossId = fd["bossid"].ToString().Trim();
            int bossId = 0;
            if (!string.IsNullOrEmpty(sbossId))
            {
                if (!Int32.TryParse(sbossId, out bossId))
                {
                    return Json(new { status = "error", msg = "Boss Invalid" });//carry garbage value
                }
                else
                {
                    var boss = _dbContext.Employees.Where(e => e.EmployeeId == bossId).FirstOrDefault();
                    if (boss != null)
                    {
                        employee.Boss = boss;
                    }
                }
            }
            _dbContext.SaveChanges();
            return Json(new { status="success"});
        }

        [HttpPost]
        public ActionResult updateEducationInfo(FormCollection fd)
        {
            string EmployeeId = fd["employeeid4edu"].ToString().Trim();
            string EducationId = fd["educationId"].ToString().Trim();
            string LblOfeducation = fd["edulblid"].ToString().Trim();
            string ExamTitle = fd["txtExamTitle"].ToString().Trim();
            string Group= fd["txtGroup"].ToString().Trim();
            string InstituteName = fd["txtInstitute"].ToString().Trim();
            string IsForign = fd["chkisForign"].ToString().Trim();
            string ResultId = fd["resultid"].ToString().Trim();
            string IsMentionMarks = fd["isMentionmarks"].ToString().Trim();
            string Marks = fd["txtMarks"].ToString().Trim();
            string Cgpa = fd["txtCGPA"].ToString().Trim();
            string Scale = fd["txtScale"].ToString().Trim();
            string YearOfPassing = fd["txtYear"].ToString().Trim();
            string Duration = fd["txtDuration"].ToString().Trim();
            string Achievements = fd["txtAchievement"].ToString().Trim();
            int employeeId = 0;
            if (Int32.TryParse(EmployeeId, out employeeId))
            {
                var employee = _dbContext.Employees.Where(e => e.EmployeeId == employeeId).FirstOrDefault();
                if (employee != null)
                {
                    int educationId = 0;
                    if (Int32.TryParse(EducationId, out educationId))
                    {
                        #region validationCheck

                        int? lblOfEducation = null;
                        int temp;
                        if (!string.IsNullOrEmpty(LblOfeducation))
                        {
                            if (!Int32.TryParse(LblOfeducation, out temp))
                            {
                                return Json(new { status = "error", msg = "Education is not Defined" });///carry garbage value
                            }
                            else
                            {
                                lblOfEducation = temp;
                            }
                        }

                        bool? isforign = null;
                        if (!string.IsNullOrEmpty(IsForign))
                        {
                            if (IsForign.ToLower().Equals("false"))
                            {
                                isforign = false;
                            }
                            else
                            {
                                isforign = true;
                            }
                        }

                        int? resultId = null;
                        if (!string.IsNullOrEmpty(ResultId))
                        {
                            if (!Int32.TryParse(ResultId, out temp))
                            {
                                return Json(new { status = "error", msg = "Result is not Defined" });///carry garbage value
                            }
                            else
                            {
                                resultId = temp;
                            }
                        }
                        bool? isMention = null;
                        if (!string.IsNullOrEmpty(IsMentionMarks))
                        {
                            if (IsMentionMarks.ToLower().Equals("false"))
                            {
                                isMention = false;
                            }
                            else
                            {
                                isMention = true;
                            }
                        }
                        decimal? marks = null;
                        decimal tempDecimal = 0.0M;
                        if (!string.IsNullOrEmpty(Marks))
                        {
                            if (!Decimal.TryParse(Marks, out tempDecimal))
                            {
                                return Json(new { status = "error", msg = "Year Of Passing is not Defined" });///carry garbage value
                            }
                            else
                            {
                                marks = tempDecimal;
                            }
                        }

                        decimal? cgpa = null;
                        if (!string.IsNullOrEmpty(Cgpa))
                        {
                            if (!Decimal.TryParse(Cgpa, out tempDecimal))
                            {
                                return Json(new { status = "error", msg = "Year Of Passing is not Defined" });///carry garbage value
                            }
                            else
                            {
                                cgpa = tempDecimal;
                            }
                        }

                        decimal? scale = null;
                        if (!string.IsNullOrEmpty(Scale))
                        {
                            if (!Decimal.TryParse(Scale, out tempDecimal))
                            {
                                return Json(new { status = "error", msg = "Year Of Passing is not Defined" });///carry garbage value
                            }
                            else
                            {
                                scale = tempDecimal;
                            }
                        }

                        int? yearOfPassing = null;
                        if (!string.IsNullOrEmpty(YearOfPassing))
                        {
                            if (!Int32.TryParse(YearOfPassing, out temp))
                            {
                                return Json(new { status = "error", msg = "Year Of Passing is not Defined" });///carry garbage value
                            }
                            else
                            {
                                yearOfPassing = temp;
                            }
                        } 
                        #endregion


                        ///////////////////////********************update Education**************************////////////////
                        if (educationId > 0)
                        {
                            var education = _dbContext.EmployeeEducations.Where(edu => edu.EmployeeEducationId == educationId).FirstOrDefault();

                            education.EducationLevelId = lblOfEducation;
                            education.ExamTitle = ExamTitle;
                            education.GroupName = Group;
                            education.InstituteName = InstituteName;
                            education.IsForeign = isforign;
                            education.ResultId = resultId;
                            education.IsMarksMention = isMention;
                            education.MarksPercent = marks;
                            education.Grade = cgpa;
                            education.Scale = scale;
                            education.YearOfPassing = yearOfPassing;
                            education.Duration = Duration;
                            education.Achievement = Achievements;
                            education.Active = false;
                            _dbContext.SaveChanges();
                            return Json(new { status = "success" });
                        }
                        ///////////////******************************end Update Education*******************/////////////////
                        else if (educationId == 0)
                        {
                            var educationAdd = new EmployeeEducation
                            {
                                EmployeeId = employeeId,
                                EducationLevelId = lblOfEducation,
                                ExamTitle = ExamTitle,
                                GroupName = Group,
                                InstituteName = InstituteName,
                                IsForeign = isforign,
                                ResultId = resultId,
                                IsMarksMention = isMention,
                                MarksPercent=marks,
                                Grade=cgpa,
                                Scale=scale,
                                YearOfPassing=yearOfPassing,
                                Duration= Duration,
                                Achievement =Achievements,
                                Active=true
                            };
                            _dbContext.EmployeeEducations.Add(educationAdd);
                            _dbContext.SaveChanges();
                            return Json(new { status = "success" });
                        }
                        
                        
                        
                        

                    }
                    else
                    {
                        return Json(new { status = "error", msg = "Education is not Defined" });
                    }
                }
                else
                {
                    return Json(new { status = "error", msg = "Employee Not Found" });
                }
            }
            else
            {
                return Json(new { status = "error", msg = "Employee Not Found" });
            }

            return Json("");
        }
        [HttpPost]
        public ActionResult EducationInfoDelete(FormCollection fd)
        {
            string EducationId = fd["empEducationIdDelete"].ToString().Trim();
            int empEducationId;
            if (Int32.TryParse(EducationId, out empEducationId))
            {
                var empEducation = _dbContext.EmployeeEducations.Where(e => e.EmployeeEducationId == empEducationId).FirstOrDefault();
                if (empEducation != null)
                {
                    _dbContext.EmployeeEducations.Remove(empEducation);
                    _dbContext.SaveChanges();
                    return Json(new { status = "success" });
                }
                else
                {
                    return Json(new { status = "success" });
                }
            }
            else
            {
                return Json(new { status = "error",msg="Education Info Not Found." });
            }
            
        }
        public JsonResult getBoss(int id)
        {
            var data = _dbContext.Employees.Where(e => e.DepartmentId == id).Select(e => new ComboItem { ID = e.EmployeeId, Name = e.Person.PersonName + " (" + e.EmployeeCode + ")" }).ToList();
            return Json(new {status="success",data=data });
        }
        [HttpPost]
        public ActionResult updateEmployeePermanentAddress(FormCollection fd)
        {
            int empId = 0;
            if (!Int32.TryParse(fd["employeeid4permanentAddress"].ToString().Trim(), out empId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = _dbContext.Employees.Where(e => e.EmployeeId == empId).FirstOrDefault();
            string addressline = fd["txtPermanentAddress"].ToString().Trim();
            if (!string.IsNullOrEmpty(addressline))
            {
                employee.Person.PermanentAddress = addressline;
            }
            string sdistrict = fd["parmanentDistid"].ToString().Trim();
            int districtid = 0;
            if (!string.IsNullOrEmpty(sdistrict))
            {
                if (!Int32.TryParse(sdistrict, out districtid))
                {
                    return Json(new { status = "error", msg = "District Invalid" });//carry garbage value
                }
                else
                {
                    employee.Person.DistrictId = districtid;
                }
            }
            string sthana = fd["permanentThanaid"].ToString().Trim();
            int thanaid = 0;
            if (!string.IsNullOrEmpty(sthana))
            {
                if (!Int32.TryParse(sthana, out thanaid))
                {
                    return Json(new { status = "error", msg = "Thana Invalid" });//carry garbage value
                }
                else
                {
                    employee.Person.ThanaId = thanaid;
                }
            }
            _dbContext.SaveChanges();
            return Json(new { status = "success" });
        }
        [HttpPost]
        public ActionResult updateEmployeePresentAddress(FormCollection fd)
        {
            int empId = 0;
            if (!Int32.TryParse(fd["employeeid4presentAddress"].ToString().Trim(), out empId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = _dbContext.Employees.Where(e => e.EmployeeId == empId).FirstOrDefault();
            string addressline = fd["txtPresentAddress"].ToString().Trim();
            if (!string.IsNullOrEmpty(addressline))
            {
                employee.Person.PresentAddress = addressline;
            }
            string sdistrict = fd["presentdistid"].ToString().Trim();
            int districtid = 0;
            if (!string.IsNullOrEmpty(sdistrict))
            {
                if (!Int32.TryParse(sdistrict, out districtid))
                {
                    return Json(new { status = "error", msg = "District Invalid" });//carry garbage value
                }
                else
                {
                    employee.Person.PresentDistrictId = districtid;
                }
            }
            string sthana = fd["presentthanaid"].ToString().Trim();
            int thanaid = 0;
            if (!string.IsNullOrEmpty(sthana))
            {
                if (!Int32.TryParse(sthana, out thanaid))
                {
                    return Json(new { status = "error", msg = "Thana Invalid" });//carry garbage value
                }
                else
                {
                    employee.Person.PresentThanaId = thanaid;
                }
            }
            _dbContext.SaveChanges();
            return Json(new { status = "success" });
        }
        [HttpPost]
        public ActionResult updateTrainingInfo(FormCollection fd)
        {
            string EmployeeId = fd["employeeid4training"].ToString().Trim();
            string TrainingId = fd["trainingid"].ToString().Trim();
            string TrainingTitle= fd["txtTrainingTitle"].ToString().Trim();
            string TopicCovered= fd["txtTopic"].ToString().Trim(); 
            string Institute= fd["txtTrainInstitute"].ToString().Trim();
            string Country= fd["txtTrainCountry"].ToString().Trim();
            string Location= fd["txtTrainLocation"].ToString().Trim();
            string Year= fd["txtTrainYear"].ToString().Trim();
            string Duration= fd["txtTrainDuration"].ToString().Trim();
            int employeeid = 0;
            if (Int32.TryParse(EmployeeId, out employeeid))
            {
                var employee = _dbContext.Employees.Where(e => e.EmployeeId == employeeid).FirstOrDefault();
                if (employee != null)
                {
                    int trainingid = 0;
                    if (Int32.TryParse(TrainingId, out trainingid))
                    {
                        int? yearOftraining = null;
                        int temp;
                        if (!string.IsNullOrEmpty(Year))
                        {
                            if (!Int32.TryParse(Year, out temp))
                            {
                                return Json(new { status = "error", msg = "Education is not Defined" });///carry garbage value
                            }
                            else
                            {
                                yearOftraining = temp;
                            }
                        }
                        if (trainingid > 0)
                        {
                            var empTraining = _dbContext.EmployeeTrainings.Where(et => et.EmployeeTrainingId == trainingid).FirstOrDefault();
                            empTraining.TrainingTitle = TrainingTitle;
                            empTraining.TopicCovered = TopicCovered;
                            empTraining.Institute = Institute;
                            empTraining.Country = Country;
                            empTraining.Location = Location;
                            empTraining.TrainingYear = yearOftraining;
                            empTraining.Duration = Duration;
                            empTraining.Active = true;
                            _dbContext.SaveChanges();
                            return Json(new { status = "success" });
                        }
                        else if (trainingid == 0)
                        {
                            var trainingadd = new EmployeeTraining
                            {
                                EmployeeId = employeeid,
                                TrainingTitle = TrainingTitle,
                                TopicCovered = TopicCovered,
                                Institute = Institute,
                                Country = Country,
                                Location = Location,
                                TrainingYear = yearOftraining,
                                Duration = Duration,
                                Active = true
                            };
                            _dbContext.EmployeeTrainings.Add(trainingadd);
                            _dbContext.SaveChanges();
                            return Json(new { status = "success" });
                        }

                    }
                    else
                    {
                        return Json(new { status = "error", msg = "Training is not Defined" });
                    }
                }
                else
                {
                    return Json(new { status = "error", msg = "Employee not Found!!" });
                }
            }
            else
            {
                return Json(new { status = "error", msg = "Employee not Found" });
            }
            return Json(new { status = "success" });
        }
        public ActionResult TrainingInfoDelete(FormCollection fd)
        {
            string TrainingId = fd["empTrainingIdDelete"].ToString().Trim();
            int trainingid = 0;
            if (Int32.TryParse(TrainingId, out trainingid))
            {
                var empTraining = _dbContext.EmployeeTrainings.Where(e => e.EmployeeTrainingId == trainingid).FirstOrDefault();
                if (empTraining != null)
                {
                    _dbContext.EmployeeTrainings.Remove(empTraining);
                    _dbContext.SaveChanges();
                    return Json(new { status = "success" });
                }
                else
                {
                    return Json(new { status = "success" });
                }
            }
            else
            {
                return Json(new { status = "error", msg = "Training Info Not Found." });
            }
        }

        public ActionResult updateWorkInfo(FormCollection fd)
        {
            string EmployeeId = fd["employeeid4working"].ToString().Trim();
            string WorkingId = fd["workingid"].ToString().Trim();
            string CompanyName = fd["txtWorkCompany"].ToString().Trim();
            string CompanyBusiness = fd["txtCompanyBusiness"].ToString().Trim();
            string CompanyLocation = fd["txtCompanyLocation"].ToString().Trim();
            string PositionHeld = fd["txtPositionHeld"].ToString().Trim();
            string Department = fd["txtCompanyDept"].ToString().Trim();
            string Responsibilities = fd["txtComResponsibilities"].ToString().Trim();
            string FromDate = fd["txtWorkFrom"].ToString().Trim();
            string ToDate = fd["txtWorkTo"].ToString().Trim();
            int employeeid = 0;
            if (Int32.TryParse(EmployeeId, out employeeid))
            {
                var employee = _dbContext.Employees.Where(e => e.EmployeeId == employeeid).FirstOrDefault();
                if (employee != null)
                {
                    int workingid = 0;
                    if (Int32.TryParse(WorkingId, out workingid))
                    {
                        DateTime tempDate = DateTime.Now;
                        DateTime? fromWorkDate = null;
                        DateTime? toWorkDate = null;
                        if (!string.IsNullOrEmpty(FromDate))
                        {
                            if (!DateTime.TryParseExact(FromDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out tempDate))
                            {
                                return Json(new { status = "error", msg = "Invalid Work From Date" });//carry invalid format value
                            }
                            else
                            {
                                fromWorkDate = tempDate;
                            }
                        }
                        if (!string.IsNullOrEmpty(ToDate))
                        {
                            if (!DateTime.TryParseExact(ToDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out tempDate))
                            {
                                return Json(new { status = "error", msg = "Invalid Work To Date" });//carry invalid format value
                            }
                            else
                            {
                                toWorkDate = tempDate;
                            }
                        }
                        if (workingid > 0)
                        {
                            var empWorking = _dbContext.EmployeeEmploymentHistories.Where(ew => ew.EmployeeEmploymentHistoryId == workingid).FirstOrDefault();
                            empWorking.CompanyName = CompanyName;
                            empWorking.CompanyBusiness = CompanyBusiness;
                            empWorking.CompanyLocation = CompanyLocation;
                            empWorking.PositionHeld = PositionHeld;
                            empWorking.Department = Department;
                            empWorking.Responsibilities = Responsibilities;
                            empWorking.FromDate = fromWorkDate;
                            empWorking.ToDate = toWorkDate;
                            empWorking.Active = true;
                            _dbContext.SaveChanges();
                            return Json(new { status = "success" });
                        }
                        else if (workingid == 0)
                        {
                            var workingadd = new EmployeeEmploymentHistory
                            {
                                EmployeeId = employeeid,
                                CompanyName=CompanyName,
                                CompanyBusiness=CompanyBusiness,
                                CompanyLocation=CompanyLocation,
                                PositionHeld=PositionHeld,
                                Department=Department,
                                Responsibilities=Responsibilities,
                                FromDate=fromWorkDate,
                                ToDate=toWorkDate,
                                Active=true
                            };
                            _dbContext.EmployeeEmploymentHistories.Add(workingadd);
                            _dbContext.SaveChanges();
                            return Json(new { status = "success" });
                        }

                    }
                    else
                    {
                        return Json(new { status = "error", msg = "Working History is not Defined" });
                    }
                }
                else
                {
                    return Json(new { status = "error", msg = "Employee not Found!!" });
                }
            }
            else
            {
                return Json(new { status = "error", msg = "Employee not Found" });
            }
            return Json(new { status = "success" });
        }

        public ActionResult WorkingInfoDelete(FormCollection fd)
        {
            string WorkingId = fd["empWorkingIdDelete"].ToString().Trim();
            int workingid = 0;
            if (Int32.TryParse(WorkingId, out workingid))
            {
                var empWorking = _dbContext.EmployeeEmploymentHistories.Where(e => e.EmployeeEmploymentHistoryId == workingid).FirstOrDefault();
                if (empWorking != null)
                {
                    _dbContext.EmployeeEmploymentHistories.Remove(empWorking);
                    _dbContext.SaveChanges();
                    return Json(new { status = "success" });
                }
                else
                {
                    return Json(new { status = "success" });
                }
            }
            else
            {
                return Json(new { status = "error", msg = "Working History Info Not Found." });
            }
        }

        public ActionResult updateCertificationInfo(FormCollection fd)
        {
            string EmployeeId = fd["employeeid4certification"].ToString().Trim();
            string CertificationId = fd["certificationid"].ToString().Trim();
            string CertificationName = fd["txtCertificationName"].ToString().Trim();
            string InstituteName = fd["txtCertificationInstitute"].ToString().Trim();
            string Location = fd["txtCertificationLocation"].ToString().Trim();
            string FromDate = fd["txtCertificationFrom"].ToString().Trim();
            string ToDate = fd["txtCertificationTo"].ToString().Trim();
            int employeeid = 0;
            if (Int32.TryParse(EmployeeId, out employeeid))
            {
                var employee = _dbContext.Employees.Where(e => e.EmployeeId == employeeid).FirstOrDefault();
                if (employee != null)
                {
                    int certificationid = 0;
                    if (Int32.TryParse(CertificationId, out certificationid))
                    {
                        DateTime tempDate = DateTime.Now;
                        DateTime? fromCertificationDate = null;
                        DateTime? toCertificationDate = null;
                        if (!string.IsNullOrEmpty(FromDate))
                        {
                            if (!DateTime.TryParseExact(FromDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out tempDate))
                            {
                                return Json(new { status = "error", msg = "Invalid Work From Date" });//carry invalid format value
                            }
                            else
                            {
                                fromCertificationDate = tempDate;
                            }
                        }
                        if (!string.IsNullOrEmpty(ToDate))
                        {
                            if (!DateTime.TryParseExact(ToDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out tempDate))
                            {
                                return Json(new { status = "error", msg = "Invalid Work To Date" });//carry invalid format value
                            }
                            else
                            {
                                toCertificationDate = tempDate;
                            }
                        }
                        if (certificationid > 0)
                        {
                            var empCertificaion = _dbContext.EmployeeCertifications.Where(ec => ec.EmployeeCertificationId == certificationid).FirstOrDefault();
                            empCertificaion.CertificationName = CertificationName;
                            empCertificaion.InstituteName = InstituteName;
                            empCertificaion.Location = Location;
                            empCertificaion.FromDate = fromCertificationDate;
                            empCertificaion.ToDate = toCertificationDate;
                            empCertificaion.Active = true;
                            _dbContext.SaveChanges();
                            return Json(new { status = "success" });
                        }
                        else if (certificationid == 0)
                        {
                            var certificationadd = new EmployeeCertification
                            {
                                EmployeeId = employeeid,
                                CertificationName=CertificationName,
                                InstituteName=InstituteName,
                                Location=Location,
                                FromDate=fromCertificationDate,
                                ToDate=toCertificationDate,
                                Active = true
                            };
                            _dbContext.EmployeeCertifications.Add(certificationadd);
                            _dbContext.SaveChanges();
                            return Json(new { status = "success" });
                        }

                    }
                    else
                    {
                        return Json(new { status = "error", msg = "Professional Certification is not Defined" });
                    }
                }
                else
                {
                    return Json(new { status = "error", msg = "Employee not Found!!" });
                }
            }
            else
            {
                return Json(new { status = "error", msg = "Employee not Found" });
            }
            return Json(new { status = "success" });
        }

        public ActionResult CertificationInfoDelete(FormCollection fd)
        {
            string CertificationId = fd["empWorkingIdDelete"].ToString().Trim();
            int certificationid = 0;
            if (Int32.TryParse(CertificationId, out certificationid))
            {
                var empCertification = _dbContext.EmployeeCertifications.Where(e => e.EmployeeCertificationId == certificationid).FirstOrDefault();
                if (empCertification != null)
                {
                    _dbContext.EmployeeCertifications.Remove(empCertification);
                    _dbContext.SaveChanges();
                    return Json(new { status = "success" });
                }
                else
                {
                    return Json(new { status = "success" });
                }
            }
            else
            {
                return Json(new { status = "error", msg = "Professional Certification Info Not Found." });
            }
        }

        public ActionResult getThanas(int id)
        {
            var data = _dbContext.Thanas.Where(t => t.DISTRICT_ID == id).Select(t => new ComboItem { ID = t.THANA_ID, Name = t.THANA_NAME }).ToList();
            return Json(new { status = "success", data = data });
        }
    }
}