namespace HrManagementAlpha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ConveyanceBill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ConveyanceBill()
        {
            ConveyanceBillRows = new HashSet<ConveyanceBillRow>();
        }

        [Key]
        public int ConveynaceBillId { get; set; }

        [StringLength(250)]
        public string JobNo { get; set; }

        [StringLength(250)]
        public string CostCentre { get; set; }

        [StringLength(250)]
        public string NameOfClient { get; set; }

        public bool? IsBillableToClient { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TotalBill { get; set; }

        public int SubmittedBy { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public bool? IsApprovedByFinance { get; set; }

        public bool? IsApprovedByManager { get; set; }

        public int? ConveyanceStatusId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SubmitDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConveyanceBillRow> ConveyanceBillRows { get; set; }

        public virtual ConveyanceStatu ConveyanceStatu { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
