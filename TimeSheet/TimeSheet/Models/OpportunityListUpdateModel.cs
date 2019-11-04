using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TimeSheet.Models
{
    public class OpportunityListUpdateModel
    {
        public int Id { get; set; }
        public int OpportunityNumber { get; set; }        
        public bool IsActive { get; set; }
        public int ProjectManagerID { get; set; }
        public int SalesManagerID { get; set; }
    }
}