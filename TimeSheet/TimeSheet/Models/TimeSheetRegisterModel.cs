using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models
{
    public class TimeSheetRegisterModel
    {

        public int Id { get; set; }
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
        //[Required(ErrorMessage = "Opportunity Is Mandatory")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime? EndDate { get; set; }
        public int? OLATarget { get; set; }
        public int? OLATravel { get; set; }
        public int? ActualQuantity { get; set; }
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string TimeSheetComments { get; set; }
        [Required(ErrorMessage = "Opportunity Is Mandatory")]
        public int? OpportunityNumberID { get; set; }
        public int OpportunityID { get; set; }
        public int? WarehouseNameId { get; set; }
        [Required(ErrorMessage = "Activity Is Mandatory")]
        public int? ServiceActivityId { get; set; }
        public string Colour { get; set; }
        [Required(ErrorMessage = "Resource Is Mandatory")]
        public int? CandidateNameId { get; set; }
        public int? ResourceID { get; set; }        
        public string CandidateName { get; set; }
        public int? StatusID { get; set; }
        [Required(ErrorMessage = "Project Is Mandatory")]
        public int? ProjectID { get; set; }
        public string JobNo { get; set; }
        [Required(ErrorMessage = "Job Is Mandatory")]
        public int? JobID { get; set; }
        public int? EmploymentTypeID { get; set; }
        [Required(ErrorMessage = "Resource selection is Mandatory")]
        public List<int?> ResourceIDs { get; set; }
        public string FullName { get; set; }
        public string Vechicles { get; set; }
        public decimal? BookingEstimatedTravelHrs { get; set; }
        public decimal? BookingEstimatedTravelCost { get; set; }
        public string BookingTravelTime { get; set; }
        public DateTime? TravelTimeStartOfStart { get; set; }
        public DateTime? TravelTimeStartOfEnd { get; set; }
        public DateTime? TravelTimeEndOfStart { get; set; }
        public DateTime? TravelTimeEndOfEnd { get; set; }
    }
}