namespace HrManagementAlpha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Thana
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Thana()
        {
            Persons = new HashSet<Person>();
            Persons1 = new HashSet<Person>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int THANA_ID { get; set; }

        [StringLength(50)]
        public string THANA_NAME { get; set; }

        public int? DISTRICT_ID { get; set; }

        public bool? ACTIVE { get; set; }

        [StringLength(50)]
        public string REMARKS { get; set; }

        public virtual District District { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person> Persons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person> Persons1 { get; set; }
    }
}
