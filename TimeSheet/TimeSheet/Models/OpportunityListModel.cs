using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class OpportunityListModel
    {
        public int Id { get; set; }
        public int OpportunityNumber { get; set; }
        public string ProjectName { get; set; }
        public bool IsActive { get; set; }
    }
}