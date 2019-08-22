using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ActivityListModel
    {
        public int Id { get; set; }       
        public string serviceActivityDescription { get; set; }
        public bool isActive { get; set; }
        public int OLA_Day { get; set; }
        public int OLA_30mins { get; set; }
        public int OLA_15mins { get; set; }
        public string olA_Filter { get; set; }
        public string serviceCategory { get; set; }
    }
}