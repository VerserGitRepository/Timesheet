using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models
{
    public class HRRegisterViewModel
    {
        public DateTime Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ServiceActivityID { get; set; }
        public int WarehouseID { get; set; }
        public int ResourceID { get; set; }
        public int ProjectID { get; set; }
        public int OpportunityID { get; set; }
        public string Colour { get; set; }
        public string TimeSheetComments { get; set; }
        public SelectList WarehouseNameList { get; set; }
        public SelectList CandidateNameList { get; set; }
        public SelectList ProjectManagerNameList { get; set; }
        public SelectList StatusList { get; set; }
        public SelectList EmploymentList { get; set; }
    }
}