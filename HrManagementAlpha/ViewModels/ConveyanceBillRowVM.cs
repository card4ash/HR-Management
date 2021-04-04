using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrManagementAlpha.ViewModels
{
    public class ConveyanceBillRowVM
    {
        
        public string rowDate {
            get;
            set;
        }
        public DateTime? getRowDate {
            get
            {
                DateTime? _rowDate;
                DateTime tempDate;
                if (DateTime.TryParseExact(rowDate.ToString(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out tempDate))
                {
                    _rowDate = tempDate;
                }
                else
                {
                    _rowDate = null;
                }
                return _rowDate;
            }
        }
        public string purpose { get; set; }
        public string fromLocation { get; set; }
        public string toLocaton { get; set; }
        public string madeOfTransport { get; set; }
        public decimal fare { get; set; }
    }
}