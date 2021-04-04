namespace HrManagementAlpha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeEducation")]
    public partial class EmployeeEducation
    {
        public int EmployeeEducationId { get; set; }

        public int EmployeeId { get; set; }

        public int? EducationLevelId { get; set; }

        [StringLength(250)]
        public string ExamTitle { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        [StringLength(350)]
        public string InstituteName { get; set; }

        public bool? IsForeign { get; set; }

        public int? ResultId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MarksPercent { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Grade { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Scale { get; set; }

        public bool? IsMarksMention { get; set; }

        public int? YearOfPassing { get; set; }
        [StringLength(50)]
        public string Duration { get; set; }

        [StringLength(450)]
        public string Achievement { get; set; }

        public bool? Active { get; set; }

        public virtual EducationLevel EducationLevel { get; set; }
        public virtual Result Result { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
