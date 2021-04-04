using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HrManagementAlpha.Models
{
    public class Result
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Result()
        {
            EmployeeEducations = new HashSet<EmployeeEducation>();
        }

        public int ResultId { get; set; }

        [StringLength(250)]
        public string ResultName { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeEducation> EmployeeEducations { get; set; }
    }
}