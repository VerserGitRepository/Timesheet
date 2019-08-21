using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ProjectionEntryModel
    {
        public int Id { get; set; }
        public int ProjectID { get; set; }
    
        public int OpportunityID { get; set; }
        public int ActivityId { get; set; }
        public int? ServiceActivityId { get; set; }
        public int WarehouseId { get; set; }
        public bool IsActive { get; set; }
        public int Quantity { get; set; }
        public DateTime Created { get; set; }
        public DateTime DateInvoiced { get; set; }
    }
}