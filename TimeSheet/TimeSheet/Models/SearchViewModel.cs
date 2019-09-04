using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class SearchViewModel
    {
        public int? ProjectID { get; set; }
        public int? OpportunityNumberID { get; set; }
        public int? WarehouseNameId { get; set; }
        public int? CandidateNameId { get; set; }
        public int? EmploymentTypeID { get; set; }
    }
}