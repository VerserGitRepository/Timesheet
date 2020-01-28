using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models
{
    public class ProfitLossModel
    {
        public List<ProfitLossModel> ProfitLossList { get; set; }
        public ProfitLossModel()
        {
            ProfitLossList = new List<ProfitLossModel>();
        }
        public string opportunityNumber { get; set; }
        public string siteAddress { get; set; }
        public string customer { get; set; }
        public string approver { get; set; }
        public string projectManager { get; set; }
        public string projectName { get; set; }
        public string salesManager { get; set; }
        public string customerContactName { get; set; }
        public string verserBranch { get; set; }
        public string costCategory { get; set; }
        public string costPerUnit { get; set; }
        public string travelCostPerUnit { get; set; }
        public string labourCostPerUnit { get; set; }
        public string variableCostPerUnit { get; set; }
        public string pmCostPerUnit { get; set; }
        public string technicianHourlyRate { get; set; }
        public string travelCostHoursPerunit { get; set; }
        public string labourCostHoursPerUnit { get; set; }
        public string pmCostHoursPerUnit { get; set; }
        public string variableCostPerUnitNA { get; set; }
        public string totalCost { get; set; }
        public string profitPerUnit { get; set; }
        public string totalProfit { get; set; }
        public string actualMarginOnOverHead { get; set; }
        public string serviceDescription { get; set; }
        public string pricePerUnit { get; set; }
        public int costmodelquantity { get; set; }
        public string projectionquantity { get; set; }
        public string totalPrice { get; set; }
        public List<AggregatedProfitLossModel> AggregatedProfitLossModel { get; set; }
        [Required(ErrorMessage = "Day Is Mandatory")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dateAllocated { get; set; }

        [Required(ErrorMessage = "Day Is Mandatory")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dateInvoiced { get; set; }
       
      
    }
}