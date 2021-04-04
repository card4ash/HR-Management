using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HrManagementAlpha.Models
{
    [Table("IDCardType")]
    public partial class IDCardType
    {
        public IDCardType()
        {
            Persons = new HashSet<Person>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDCardTypeId { get; set; }
        [StringLength(50)]
        public string IDCardTypeName { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person> Persons { get; set; }
    }
}