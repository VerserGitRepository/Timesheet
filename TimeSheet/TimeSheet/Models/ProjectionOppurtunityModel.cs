using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models
{
    public class ProjectionOppurtunityModel
    {
       
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int OppurtunityId { get; set; }
        public int OppurtunityNumber { get; set; }
       
        public bool? IsActive { get; set; }
        public int ServiceActivityId { get; set; }
        public string ServiceActivityDescription { get; set; }
        private DateTime dateAllocated;
        public DateTime? DateAllocated { get { return dateAllocated; } set { if (value == null) dateAllocated = DateTime.MinValue; } }
        private DateTime dateInvoiced;
        public DateTime? DateInvoiced { get { return dateInvoiced; } set { if (value == null) dateInvoiced = DateTime.MinValue; } }
        public string VerserBranch { get; set; }
        public string Activity { get; set; }
        public int ActualQuantity { get; set; }

    }
}