using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ActivityListItems
    {
        public int Id { get; set; }       
        public string Activity { get; set; }
        public int OLA_Day { get; set; }
        public int OLA_30mins { get; set; }
        public int OLA_15mins { get; set; }
        public string OLA_Filters { get; set; }
    }
}