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
        public int OpportinutyId { get; set; }
        public int OpportunityNumber { get; set; }
       
        public bool? IsActive { get; set; }
        public int ServiceActivityId { get; set; }
        public int ActivityId { get; set; }
        public string ServiceActivityDescription { get; set; }
        public DateTime? DateAllocated { get; set; }
        public DateTime? DateInvoiced { get; set; }
        public string VerserBranch { get; set; }
        public string Activity { get; set; }
        public int ActualQuantity { get; set; }
        public int wareHouseId { get; set; }
        public string wareHouseName { get; set; }
        public string Comments { get; set; }
        public string ProjectManager { get; set; }

    }
}