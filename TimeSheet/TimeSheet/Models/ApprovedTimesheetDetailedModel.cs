using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ApprovedTimesheetDetailedModel
    {
        public int EmployeeID { get; set; }
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public string Day3 { get; set; }
        public string Day4 { get; set; }
        public string Day5 { get; set; }
        public string project { get; set; }
        public string EmployeeName { get; set; }
        public string AfterHoursShift { get; set; }
        public string Branch { get; set; }
        public string Total { get; set; }

    }
}