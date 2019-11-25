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
       
        public string Project { get; set; }

        public int? Opportunity { get; set; }
        public string ProjectManager { get; set; }
        public string SalesManager { get; set; }
        public string Activity { get; set; }

        public string Facility { get; set; }
        
       
        public string DateInvoiced { get; set; }

        public int ProjectionQuantity { get; set; }
        public int CostModelQuantity { get; set; }
        public string DateAllocated { get; set; }
        public double? priceperUnit { get; set; }
        public int Quantity { get; set; }        
        public int ActualQuantity { get; set; }
        public double EstimatedRevenue { get { return Math.Round((Quantity * Convert.ToDouble(priceperUnit)), 2, MidpointRounding.ToEven); } set { } }
        public double ActualRevenue { get { return Math.Round((ActualQuantity * Convert.ToDouble(priceperUnit)), 2, MidpointRounding.ToEven); } set { } }  
        public double Gap { get; set; }
        public string Comments { get; set; }
        
       

    }
}