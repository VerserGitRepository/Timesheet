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
        //[Required]
        //[DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]

        public DateTime? StartTime { get; set; }

        //[Required]
        //[DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]

        public DateTime? EndTime { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime? Day { get; set; }
        
   
    }
}