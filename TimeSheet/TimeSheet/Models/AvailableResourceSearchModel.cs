using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models
{
    public class AvailableResourceSearchModel
    {
        public List<AvailableResourceSearchModel> AvailableResourceList { get; set; }
        public AvailableResourceSearchModel()
        {
            AvailableResourceList = new List<AvailableResourceSearchModel>();
        }
        [Required]
        public int? WarehouseId { get; set; }
        public SelectList WarehouseNameList { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }

    }
}