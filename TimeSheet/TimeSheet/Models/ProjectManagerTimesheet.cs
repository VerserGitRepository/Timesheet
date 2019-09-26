using System;
using System.Collections.Generic;
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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
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
    }
}