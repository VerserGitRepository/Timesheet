using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace TimeSheet.Models
{
    public class TimeSheetViewModel
    {
        public List<TimeSheetViewModel> CandidateTimeSheetList { get; set; }
        public TimeSheetViewModel()
        {
            CandidateTimeSheetList = new List<TimeSheetViewModel>();
        }
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }
        public string JobNo { get; set; }
        public int? JobID { get; set; }
        public int OpportunityNumber { get; set; }
        public int? OLATarget { get; set; }
        public int? ActualQuantity { get; set; }
        public string Customer { get; set; }
        [DataType(DataType.MultilineText)]
        public string TimeSheetComments { get; set; }
        public string WarehouseName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Day { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        public int? OpportunityID { get; set; }
        public SelectList OpportunityNumberList { get; set; }
        public int? WarehouseID { get; set; }
        public SelectList WarehouseNameList { get; set; }
        public int? ServiceActivityID { get; set; }
        public string Activity { get; set; }
        public string Colour { get; set; }
        public SelectList ActivityList { get; set; }
        public int? ResourceID { get; set; }
        [Required]
        public List<int?> ResourceIDs { get; set; }
        public string CandidateName { get; set; }
        public SelectList CandidateNameList { get; set; }
        public SelectList StatusList { get; set; }
        public int? StatusID { get; set; }
        public string Status { get; set; }
        public SelectList Projectlist { get; set; }
        public int? ProjectID { get; set; }
        public string ProjectName { get; set; }
        public virtual TimeSheetRegisterModel TimeSheetRegisterModel { get;set;}
    }   
}