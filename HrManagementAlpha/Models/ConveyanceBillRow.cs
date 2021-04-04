namespace HrManagementAlpha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConveyanceBillRow")]
    public partial class ConveyanceBillRow
    {
        [Key]
        public int RowId { get; set; }

        public int ConveyanceBillId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RowDate { get; set; }

        [StringLength(250)]
        public string Purpose { get; set; }

        [StringLength(250)]
        public string FromLocation { get; set; }

        [StringLength(250)]
        public string ToLocation { get; set; }

        [StringLength(50)]
        public string MadeOfTransport { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Fare { get; set; }

        public virtual ConveyanceBill ConveyanceBill { get; set; }
    }
}
