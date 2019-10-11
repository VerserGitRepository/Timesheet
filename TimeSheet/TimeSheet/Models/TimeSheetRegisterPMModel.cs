using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models
{
    public class TimeSheetRegisterPMModel
    {

        
        [Required(ErrorMessage = "StartTime Is Mandatory")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }
        [Required(ErrorMessage = "EndTime Is Mandatory")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }
        [Required(ErrorMessage = "Day Is Mandatory")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Day { get; set; }
        
        
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string TimeSheetComments { get; set; }
        [Required(ErrorMessage = "Opportunity Is Mandatory")]
        public int? OpportunityID { get; set; }
        
        [Required(ErrorMessage = "Activity Is Mandatory")]
        public int? ServiceActivityID { get; set; }
        public string Colour { get; set; }
        [Required(ErrorMessage = "Resource Is Mandatory")]
        public int? ResourceID { get; set; }
        
        
        [Required(ErrorMessage = "Project Is Mandatory")]
        public int? ProjectID { get; set; }
        public string AdditionalActivities { get; set; }

    }
}