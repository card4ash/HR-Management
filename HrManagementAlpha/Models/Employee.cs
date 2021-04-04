namespace HrManagementAlpha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            EmployeeCertifications = new HashSet<EmployeeCertification>();
            EmployeeEducations = new HashSet<EmployeeEducation>();
            EmployeeEmploymentHistories = new HashSet<EmployeeEmploymentHistory>();
            EmployeeTrainings = new HashSet<EmployeeTraining>();
            ConveyanceBills = new HashSet<ConveyanceBill>();
        }

        public int EmployeeId { get; set; }

        public int PersonId { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public int? ProjectId { get; set; }

        public int? OfficeId { get; set; }

        public int? DesignationId { get; set; }

        public int? DepartmentId { get; set; }


        [StringLength(50)]
        public string EmployeeCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? JoinDate { get; set; }

        public int? CreatedBy { get; set; }

        public bool? Active { get; set; }

        public virtual Department Department { get; set; }

        public virtual Designation Designation { get; set; }

        public virtual Office Office { get; set; }
        public virtual Project Project { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeCertification> EmployeeCertifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeEducation> EmployeeEducations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeEmploymentHistory> EmployeeEmploymentHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConveyanceBill> ConveyanceBills { get; set; }

        public virtual Person Person { get; set; }
        public virtual Employee Boss { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeTraining> EmployeeTrainings { get; set; }
    }
}
