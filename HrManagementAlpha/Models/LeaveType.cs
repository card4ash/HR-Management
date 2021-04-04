using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HrManagementAlpha.Models
{
    public class LeaveType
    {
        [Key]
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
    }
}