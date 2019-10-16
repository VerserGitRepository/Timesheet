using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models
{
    public class ProjectionOppurtunityExportModel
    {
       
        public string ProjectName { get; set; }

        public int? OpportunityNumber { get; set; }
       
        public int? ServiceActivityId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateInvoiced { get; set; }        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateAllocated { get; set; }
        public int? Quantity { get; set; }        
        public string Activity { get; set; }
        public int? ActualQuantity { get; set; }
        public string wareHouseName { get; set; }
        public string Comments { get; set; }
        public string ProjectManager { get; set; }
       

    }
}