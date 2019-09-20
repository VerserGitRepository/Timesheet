using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class CompletedtimesheetExportModel
    {
        public string ProjectName { get; set; }
        public string CandidateName { get; set; }
        public int OpportunityNumber { get; set; }
        public string Activity { get; set; }
        public string WarehouseName { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }
        public string JobNo { get; set; }     
        public int? OLATarget { get; set; }
        public int? ActualQuantity { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Day { get; set; }       
        public string Status { get; set; }
        public string TimeSheetComments { get; set; }
        public double Hours { get; set; }

    }
}