using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrManagementAlpha.Models
{
    public class ComboItem
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int ParentID { get; set; }
    }
}