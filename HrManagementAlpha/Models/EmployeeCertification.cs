namespace HrManagementAlpha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeCertification")]
    public partial class EmployeeCertification
    {
        public int EmployeeCertificationId { get; set; }

        public int EmployeeId { get; set; }

        [StringLength(250)]
        public string CertificationName { get; set; }

        [StringLength(250)]
        public string InstituteName { get; set; }

        [StringLength(250)]
        public string Location { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FromDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }

        public bool? Active { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
