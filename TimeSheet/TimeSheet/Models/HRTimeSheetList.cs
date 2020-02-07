using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models
{
    public class HRTimeSheetList
    {
        public List<HRTimeSheetList> HRTimeSheets { get; set; }
        public HRTimeSheetList()
        {
            HRTimeSheets = new List<HRTimeSheetList>();
        }
        public int id { get; set; }
        public int OpportunityID { get; set; }
        public int ProjectID { get; set; }
        public int StatusID { get; set; }
        public int WarehouseID { get; set; }
        public int ResourceID { get; set; }
        public int BreakHours { get; set; }
        public int ADPEmployeeID { get; set; }
        public string BreakTime { get; set; }
        public string Status { get; set; }
        public int ServiceActivityID { get; set; }
        public DateTime Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Colour { get; set; }
        public string CandidateName { get; set; }
        public SelectList StatusList { get; set; }
        public string Activity { get; set; }
        public string TimesheetComments { get; set; }
        public string AdditionalActivities { get; set; }
        public string OpportunityNumber { get; set; }
        public string OpportunityName { get; set; }
    }
}