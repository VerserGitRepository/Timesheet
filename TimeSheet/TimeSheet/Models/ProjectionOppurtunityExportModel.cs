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
        public double? priceperUnit { get; set; }
        public int ProjectionQuantity { get; set; }
        public int CostModelQuantity { get; set; }      
           
        public int ActualQuantity { get; set; }

        public double CostModelRevenue { get { return Math.Round((CostModelQuantity * Convert.ToDouble(priceperUnit)), 2, MidpointRounding.ToEven); } set { } }
        public double ProjectionRevenue { get { return Math.Round((ProjectionQuantity * Convert.ToDouble(priceperUnit)), 2, MidpointRounding.ToEven); } set { } }

        public double Gap { get; set; }
        public string Comments { get; set; }
        
       

    }
}