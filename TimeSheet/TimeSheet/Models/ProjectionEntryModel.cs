using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ProjectionEntryModel
    {
        public int Id { get; set; }
        public int ProjectID { get; set; }
    
        public int OpportunityID { get; set; }
        public int OpportunityNumber { get; set; }
        public int ActivityId { get; set; }
        public int? ServiceActivityId { get; set; }
        [Required]
        public int WarehouseId { get; set; }
        public bool IsActive { get; set; }
        public int Quantity { get; set; }
        [Required]
        public DateTime? DateModified { get; set; }
        public DateTime? Created { get; set; }
        [Required]
        public DateTime? DateInvoiced { get; set; }
        public string Comments { get; set; }
    }
}