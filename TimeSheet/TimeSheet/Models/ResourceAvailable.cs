using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TimeSheet.Models
{
    public class ResourceAvailable
    {
        public List<ResourceAvailable> ResourceAvailableList { get; set; }
        public ResourceAvailable()
        {
            ResourceAvailableList = new List<ResourceAvailable>();
        }
        public string ResourceName { get; set; }
        public string Warehouse { get; set; }
        public int? Hours { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? availableDate { get; set; }
        public DateTime? availableStarTime { get; set; }
        public DateTime? availableEndTime { get; set; }
        public virtual TimeSheetRegisterModel TimeSheetRegisterModel { get; set; }
    }
}