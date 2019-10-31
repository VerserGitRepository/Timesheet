using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models
{
    public class ProjectManagerTimesheet
    {
        public List<ProjectManagerTimesheet> PMTimeSheetList { get; set; }
        public ProjectManagerTimesheet()
        {
            PMTimeSheetList = new List<ProjectManagerTimesheet>();
        }
        public int Id { get; set; }
        public int ResourceID { get; set; }
        public string CandidateName { get; set; }
        public int ServiceActivityID { get; set; }
        public string Activity { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public string TimeSheetComments { get; set; }
        public int WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int OpportunityNumber { get; set; }
        public int OpportunityID { get; set; }
        public string Colour { get; set; }
        public string FullName { get; set; }
        public string EmployeementType { get; set; }
        public int? ADPEmployeeID { get; set; }
        public virtual PMRegisterViewModel PMRegisterViewModel { get; set; }
        public SelectList WarehouseNameList { get; set; }
        public SelectList CandidateNameList { get; set; }
        public SelectList ProjectManagerNameList { get; set; }
        public SelectList StatusList { get; set; }
        public SelectList EmploymentList { get; set; }
        public SelectList OpportunityNumberList { get; internal set; }
        public SelectList Projectlist { get; internal set; }
        public SelectList ActivityList { get; internal set; }
        public string jsonResources { get; internal set; }
        public string jsonEvents { get; internal set; }
        public string AdditionalActivities { get; set; }
        public int BreakHours { get; set; }
        public string BreakTime { get; set; }
    }
}