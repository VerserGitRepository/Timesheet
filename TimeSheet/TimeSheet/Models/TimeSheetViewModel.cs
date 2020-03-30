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
        [Required]
        public string JobNo { get; set; }
        [Required]
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
        [Required(ErrorMessage = "Opportunity Is Mandatory")]
        public int OpportunityID { get; set; }
        public SelectList OpportunityNumberList { get; set; }
        public SelectList SalesForceOpportunityNumberList { get; set; }
        public int? WarehouseID { get; set; }
        public SelectList WarehouseNameList { get; set; }
        [Required(ErrorMessage = "Activity selection is Mandatory")]
        public int? ServiceActivityID { get; set; }
        public string Activity { get; set; }
        public string Colour { get; set; }
        public SelectList ActivityList { get; set; }
        public SelectList HRActivityList { get; set; }
        public int? ResourceID { get; set; }
        [Required(ErrorMessage = "Resource selection is Mandatory")]
        public List<int?> ResourceIDs { get; set; }
        public string CandidateName { get; set; }
        public SelectList CandidateNameList { get; set; }
        public SelectList HRBookingResourceList { get; set; }
        public int? HRBookingResourceId { get; set; }        
        public SelectList ProjectManagerNameList { get; set; }
        public SelectList StatusList { get; set; }
        public int? StatusID { get; set; }
        public string Status { get; set; }
        public SelectList Projectlist { get; set; }
        public SelectList SalesForceProjectlist { get; set; }
        [Required(ErrorMessage = "Project Is Mandatory")]
        public int? ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectManager { get; set; }
        public int? EmploymentTypeID { get; set; }
        public SelectList EmploymentList { get; set; }
        public virtual TimeSheetRegisterModel TimeSheetRegisterModel { get;set;}
        public virtual PMRegisterViewModel PMRegisterViewModel { get; set; }
        public string jsonResources { get; set; }
        public string jsonEvents { get; set; }
        public string StartTimeString { get { return Convert.ToDateTime(this.StartTime).ToString("yyyy-MM-ddTHH:mm:ss"); }  }
        public string EndTimeString { get { return Convert.ToDateTime(this.EndTime).ToString("yyyy-MM-ddTHH:mm:ss"); } }
        public bool HasUserPermissionsToEdit { get; set; }
        public string AdditionalActivities { get; set; }
        public string Vechicles { get; set; }
        public int BreakHours { get; set; }
        public string BreakTime { get; set; }
        public string ScreenName { get; set; }
    }   
}