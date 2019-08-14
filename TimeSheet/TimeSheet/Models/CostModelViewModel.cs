using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class CostModelViewModel
    {

        public string OpportunityNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SiteAddress { get; set; }
        public string Customer { get; set; }
        public string Approver { get; set; }
        public string ProjectManager { get; set; }
        public string SalesManager { get; set; }
        public string CustomerContactName { get; set; }
        public string VerserBranch { get; set; }
        public string CostCategory { get; set; }
        public string CostPerUnit { get; set; }
        public string TravelCostPerUnit { get; set; }
        public string LabourCostPerUnit { get; set; }
        public string VariableCostPerUnit { get; set; }
        public string PMCostPerUnit { get; set; }
        public string TechnicianHourlyRate { get; set; }
        public string TravelCostHoursPerunit { get; set; }
        public string LabourCostHoursPerUnit { get; set; }
        public string PMCostHoursPerUnit { get; set; }
        public string VariableCostPerUnitNA { get; set; }
        public string TotalCost { get; set; }
        public string ProfitPerUnit { get; set; }
        public string TotalProfit { get; set; }
        public string ActualMarginOnOverHead { get; set; }
        public string ServiceDescription { get; set; }
        public string PricePerUnit { get; set; }
        public string Quantity { get; set; }
        public string TotalPrice { get; set; }

    }
}