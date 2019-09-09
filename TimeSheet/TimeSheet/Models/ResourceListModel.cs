using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ResourceListModel
    {
        public int id { get; set; }
        public string ResourceName { get; set; }
        public string Warehouse { get; set; }
        public bool IsActive { get; set; }
        public string title { get; set; }
        public string eventColor { get; set; }

        public string ActivityDescription { get; set; }

        public string ProjectName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime{ get; set; }
        public string ProjectManager { get; set; }

    }
}