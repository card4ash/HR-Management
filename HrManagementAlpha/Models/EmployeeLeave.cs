using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrManagementAlpha.Models
{
    public class EmployeeLeave
    {
        public int EmployeeLeaveId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime LeaveApplyDate { get; set; }
        public DateTime  LeaveBeginDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public int NoOfDays { get; set; }
        public int LeaveStatusId { get; set; }
        public string Purpose { get; set; }
        public string RelativeName { get; set; }
        public string Relation { get; set; }
        public string EmployeeRemarks { get; set; }
        public string SupervisorComments { get; set; }
        public string AdministrativeNotes { get; set; }
        public virtual Employee Employee { get; set; }
    }
}