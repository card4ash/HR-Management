namespace HrManagementAlpha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MaritalStatus")]
    public partial class MaritalStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaritalStatusId { get; set; }

        [StringLength(50)]
        public string MaritalStatusName { get; set; }
    }
}
