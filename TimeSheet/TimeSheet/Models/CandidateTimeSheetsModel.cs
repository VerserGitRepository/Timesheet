using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace TimeSheet.Models
{
    public class CandidateTimeSheetsModel
    {
        public List<CandidateTimeSheetsModel> CandidateTimeSheetList { get; set; }
        public CandidateTimeSheetsModel()
        {
            CandidateTimeSheetList = new List<CandidateTimeSheetsModel>();
        }
        public int Id { get; set; }
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        
        public DateTime? StartTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }

        public string JobNo { get; set; }
        public string OpportunityNumber { get; set; }
        public int? OLATarget { get; set; }
        public int ActualQuantity { get; set; }
        public string Customer { get; set; }
        public string TimeSheetComments { get; set; }
        public string WarehouseName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Day { get; set; }

        public int? CustomerID { get; set; }
        public SelectList Customerlist { get; set; }

        public int? OpportunityNumberID { get; set; }
        public SelectList OpportunityNumberList { get; set; }

        public int? WarehouseNameId { get; set; }
        public SelectList WarehouseNameList { get; set; }

        public int? ActivityId { get; set; }
        public string Activity { get; set; }
        public SelectList ActivityList { get; set; }

        public int? CandidateNameId { get; set; }
        public string CandidateName { get; set; }
        public SelectList CandidateNameList { get; set; }
    }
}