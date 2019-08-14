using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ProjectionViewModel
    {
        public int Id { get; set; }
        public int OpportinutyId { get; set; }
        public string Customer { get; set; }
        public string OpportunityNumber { get; set; }
        public string ProjectManager { get; set; }
        public string SiteAddress { get; set; }
        public string CustomerContactName { get; set; }
        public string VerserBranch { get; set; }
        public string ServiceBundle { get; set; }
        public int Quantity { get; set; }
        public Nullable<decimal> PriceperUnit { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
    }
}