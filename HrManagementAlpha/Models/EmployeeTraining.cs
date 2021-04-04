namespace HrManagementAlpha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeTraining")]
    public partial class EmployeeTraining
    {
        public int EmployeeTrainingId { get; set; }

        public int EmployeeId { get; set; }

        [StringLength(250)]
        public string TrainingTitle { get; set; }

        [StringLength(250)]
        public string TopicCovered { get; set; }

        [StringLength(250)]
        public string Institute { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        public int? TrainingYear { get; set; }

        [StringLength(50)]
        public string Duration { get; set; }

        public bool? Active { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
