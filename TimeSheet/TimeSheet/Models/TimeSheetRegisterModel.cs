using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class TimeSheetRegisterModel
    {
       
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Day { get; set; }

        public int? OLATarget { get; set; }
        public int? ActualQuantity { get; set; }
        public string TimeSheetComments { get; set; }        
        [Required]
        public int? OpportunityNumberID { get; set; }
        [Required]
        public int? WarehouseNameId { get; set; }
        [Required]
        public int? ServiceActivityId { get; set; }       
        public string Colour { get; set; }
        [Required]
        public int? CandidateNameId { get; set; }
        public string CandidateName { get; set; }
        public int? StatusID { get; set; }
        [Required]
        public int? ProjectID { get; set; }       
 		public string JobNo { get; set; }
        public int? JobID { get; set; }
    }
}