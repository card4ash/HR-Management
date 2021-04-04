namespace HrManagementAlpha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducationLevel")]
    public partial class EducationLevel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EducationLevel()
        {
            EmployeeEducations = new HashSet<EmployeeEducation>();
        }

        public int EducationLevelId { get; set; }

        [StringLength(250)]
        public string EducationLevelName { get; set; }

        public bool? Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeEducation> EmployeeEducations { get; set; }
    }
}
