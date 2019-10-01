using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class UpdateProjectManagerModel
    {
        public int OpportunityNumberID { get; set; }
        public int ProjectID { get; set; }
        public int WareHouseID { get; set; }
        public int ResourceID { get; set; }
        public int ServiceActivityID { get; set; }        
        public string Colour { get; set; }
        public int? StatusID { get; set; }
        public string TimeSheetComments { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? Day { get; set; }
        public DateTime? EndDate { get; set; }
        
    }
}