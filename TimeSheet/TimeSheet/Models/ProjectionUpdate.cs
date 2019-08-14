using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ProjectionUpdate
    {
        public int Id { get; set; }       
        public int Quantity { get; set; }
        public bool? IsActive { get; set; }
        public string Comments { get; set; }
        public string DateInvoiced { get; set; }
    }
}