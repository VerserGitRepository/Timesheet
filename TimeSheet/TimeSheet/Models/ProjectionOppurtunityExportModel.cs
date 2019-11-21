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
        public string ProjectManager { get; set; }
        public string Activity { get; set; }
        [Display(Name = "Facility")]
        public string Warehouse { get; set; }
        
       
        public string DateInvoiced { get; set; }        
        
       
        public string DateAllocated { get; set; }
        public double? priceperUnit { get; set; }
        public int Quantity { get; set; }        
        public int ActualQuantity { get; set; }
        public double EstimatedRevenue { get { return Math.Round((Quantity * Convert.ToDouble(priceperUnit)), 2, MidpointRounding.ToEven); } set { } }
        public double ActualRevenue { get { return Math.Round((ActualQuantity * Convert.ToDouble(priceperUnit)), 2, MidpointRounding.ToEven); } set { } }        
        public string Comments { get; set; }
        
       

    }
}