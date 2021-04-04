namespace HrManagementAlpha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeEmploymentHistory")]
    public partial class EmployeeEmploymentHistory
    {
        public int EmployeeEmploymentHistoryId { get; set; }

        public int EmployeeId { get; set; }

        [StringLength(250)]
        public string CompanyName { get; set; }

        public int? CompanyBusinessId { get; set; }

        [StringLength(250)]
        public string CompanyBusiness { get; set; }

        [StringLength(250)]
        public string CompanyLocation { get; set; }

        [StringLength(250)]
        public string PositionHeld { get; set; }

        [StringLength(250)]
        public string Department { get; set; }

        [StringLength(500)]
        public string Responsibilities { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FromDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }

        public bool? IsCurrentlyWorking { get; set; }

        public bool? Active { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
