namespace HrManagementAlpha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ConveyanceStatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ConveyanceStatu()
        {
            ConveyanceBills = new HashSet<ConveyanceBill>();
        }

        [Key]
        public int ConveyanceStatusId { get; set; }

        [StringLength(500)]
        public string ConveyanceStatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConveyanceBill> ConveyanceBills { get; set; }
    }
}
