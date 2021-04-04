namespace HrManagementAlpha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Persons")]
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            
        }

        public int PersonId { get; set; }

        [StringLength(150)]
        public string PersonName { get; set; }

        [StringLength(150)]
        public string FatherName { get; set; }

        [StringLength(150)]
        public string MotherName { get; set; }

        [StringLength(400)]
        public string PresentAddress { get; set; }

        public int? PresentDistrictId { get; set; }

        public int? PresentThanaId { get; set; }

        [StringLength(400)]
        public string PermanentAddress { get; set; }

        public int? DistrictId { get; set; }

        public int? ThanaId { get; set; }

        [StringLength(50)]
        public string ContactNo { get; set; }

        [StringLength(50)]
        public string SecondContactNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        public int? IDCardTypeId { get; set; }

        [StringLength(50)]
        public string IDCardNo { get; set; }

        public int? GenderId { get; set; }
       

        public int? MaritalStatusId { get; set; }

        public int? ReligionId { get; set; }

        [StringLength(50)]
        public string Nationality { get; set; }
        [StringLength(150)]
        public string ProfilePhoto { get; set; }

        public bool? IsActive { get; set; }

        public virtual District District { get; set; }

        public virtual District District1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual Thana Thana { get; set; }

        public virtual Thana Thana1 { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Religion Religion { get; set; }
        public virtual IDCardType IDCardType { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        
    }
}
