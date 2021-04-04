using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HrManagementAlpha.Models
{
    public class LeaveStatistics
    {
        [Key]
        public int LeaveStatisticsId { get; set; }
        public int LeaveTypeId { get; set; }
        public int EmployeeId { get; set; }
        public int ReaminingLeave { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}