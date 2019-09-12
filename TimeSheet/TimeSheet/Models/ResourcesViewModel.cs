using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ResourcesViewModel
    {
        public int Id { get; set; }
        public string ResourceName { get; set; }
        public string ResourceType { get; set; }
        public string value { get; set; }
    }
}