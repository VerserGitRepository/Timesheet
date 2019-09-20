using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models
{
    public class AggregatedCompletedTimesheetModel
    {
       
    
        public double Hours { get; set; }
        public string CandidateName { get; set; }
        public string ProjectManager { get; set; }
        public int? EmploymentTypeID { get; set; }
        public string PayCycle { get; set; }


    }
}
