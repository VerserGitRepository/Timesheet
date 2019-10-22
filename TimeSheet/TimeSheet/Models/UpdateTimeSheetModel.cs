using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class UpdateTimeSheetModel
    {
        public int Id { get; set; }
        public int? StatusID { get; set; }
        public int? ActualQuantity { get; set; }
        public string TimeSheetComments { get; set; }      
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? Day { get; set; }
        public DateTime? EndDate { get; set; }
        public string FullName { get; set; }
        public int BreakHours { get; set; }
        public string BreakTime { get; set; }
    }
}