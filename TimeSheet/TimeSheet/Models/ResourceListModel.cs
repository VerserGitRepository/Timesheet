using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ResourceListModel
    {
        public int Id { get; set; }
        public string ResourceName { get; set; }
        public string Warehouse { get; set; }
        public bool IsActive { get; set; }

    }
}