using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class CompletedtimesheetExcelExportModel
    {
        public string ProjectName { get; set; }
        public string CandidateName { get; set; }
        public int OpportunityNumber { get; set; }
        public string Activity { get; set; }
        public string WarehouseName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string JobNo { get; set; }     
        public int? OLATarget { get; set; }
        public int? ActualQuantity { get; set; }

        public string Day { get; set; }       
        public string Status { get; set; }
        public string TimeSheetComments { get; set; }
        public double Hours { get; set; }

    }
}