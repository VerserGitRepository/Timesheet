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
        public string DateAllocated { get; set; }
        public decimal CostperUnit { get; set; }
        public decimal? priceperUnit { get; set; }
        public decimal ProjectionQuantity { get; set; }
        public decimal CostModelQuantity { get; set; }      
           
        public int ActualQuantity { get; set; }

        public decimal CostModelRevenue { get { return Math.Round((CostModelQuantity * Convert.ToDecimal(priceperUnit)), 2, MidpointRounding.ToEven); } set { } }
        public decimal ProjectionRevenue { get { return Math.Round((ProjectionQuantity * Convert.ToDecimal(priceperUnit)), 2, MidpointRounding.ToEven); } set { } }

        public decimal Gap { get; set; }
        public string Comments { get; set; }
        
       

    }
}