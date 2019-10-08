using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class UpdateProjectManagerModel
    {
        public int Id { get; set; }
        public string TimeSheetComments { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? Day { get; set; }
        public string FullName { get; set; }

    }
}