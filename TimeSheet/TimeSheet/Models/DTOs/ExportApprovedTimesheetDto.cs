using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeSheet.Models.DTOs
{
    public class ExportApprovedTimesheetDto
    {       
            public int Id { get; set; }         
            [DataType(DataType.Time)]
            [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
            public DateTime? StartTime { get; set; }    
            [DataType(DataType.Time)]
            [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
            public DateTime? approvedDate { get; set; }    
            [DataType(DataType.Time)]
            [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
            public DateTime? EndTime { get; set; }        
            public string JobNo { get; set; }           
            public int OpportunityNumber { get; set; }
            public int? OLATarget { get; set; }
            public int? ActualQuantity { get; set; }
            public string Customer { get; set; }
            public string TimeSheetComments { get; set; }
            public string WarehouseName { get; set; }      
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? Day { get; set; }        
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? EndDate { get; set; }
            public string Activity { get; set; }
            public string CandidateName { get; set; }
            public string Status { get; set; }
            public string ProjectName { get; set; }
            public string ProjectManager { get; set; }
            public string EmployeementType { get; set; }
            public int? AdpEmployeeID { get; set; }
            public string PayFrequency { get; set; }
            public int BreakHours { get; set; }
            public string BookedBy { get; set; }
            public string ApprovedBy { get; set; }
            public string BreakTime { get; set; }
            public double TotalHours { get { return (EndTime.Value.Subtract(StartTime.Value).TotalMinutes / 60); } set { } }
            public double TotalWorkedHours { get { return ((EndTime.Value.Subtract(StartTime.Value).TotalMinutes - BreakHours) / 60); } set { } }

        }
    }
