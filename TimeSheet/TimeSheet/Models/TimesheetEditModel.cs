using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models.JMSEntities
{
    public class TimesheetEditModel
    {
      
            public int Id { get; set; }
            [Required]
            [DataType(DataType.Time)]
            [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
            public DateTime? StartTime { get; set; }

            [Required]
            [DataType(DataType.Time)]
            [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
            public DateTime? EndTime { get; set; }
            
            public int OpportunityNumber { get; set; }
            
            public int? ActualQuantity { get; set; }
            public string Customer { get; set; }
            [DataType(DataType.MultilineText)]
            public string TimeSheetComments { get; set; }
            public string WarehouseName { get; set; }
            [Required]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? Day { get; set; }
            public int? OpportunityID { get; set; }
            public int? ServiceActivityID { get; set; }
            public string Activity { get; set; }
  
            public SelectList StatusList { get; set; }
            public int? StatusID { get; set; }
            public string Status { get; set; }
           
            public int? ProjectID { get; set; }
            public string ProjectName { get; set; }
        
    }
}