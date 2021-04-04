using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrManagementAlpha.ViewModels
{
    public class ConveyanceSearchResult
    {
        public string JobNo { get; set; }
        public string CostCentre { get; set; }
        public string NameOfClient { get; set; }
        public string SubmitedBy { get; set; }
        public string NoOfRow { get; set; }
        public decimal TotalFare { get; set; }
        public int ConveyanceId { get; set; }
    }
}