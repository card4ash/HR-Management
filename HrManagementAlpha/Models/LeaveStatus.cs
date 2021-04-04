using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HrManagementAlpha.Models
{
    public class LeaveStatus
    {
        [Key]
        public int LeaveStatusId { get; set; }
        public string LeaveStatusName { get; set; }
    }
}