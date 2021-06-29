using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ExportableExpenseRequests
    {
		public int ExpenseClaimId { get; set; }
		public DateTime ExpenseDateOfClaim { get; set; }
		public string EmployeeName { get; set; }
		public string ApprovedBy { get; set; }
		public string ExpenseClaimType { get; set; }		
		public string SharePointFileLocation { get; set; }
		public decimal? TravelTotal { get; set; }
		public decimal? MealsTotal { get; set; }
		public decimal? AccommodationTotal { get; set; }
		public decimal? ToolsTotal { get; set; }
		public decimal? OtherTotal { get; set; }
		public decimal ExpenseTotal { get; set; }
		public DateTime? ClaimDate { get; set; }
		public string Supplier { get; set; }
		public string ItemDescription { get; set; }
		public string Project { get; set; }
		public string Branch { get; set; }
		public string PONumber { get; set; }
		public decimal? TravelCost { get; set; }
		public decimal? MealsCost { get; set; }
		public decimal? AccommodationCost { get; set; }
		public decimal? ToolsCost { get; set; }
		public decimal? OtherCost { get; set; }
		public decimal? WHTools_Consumables { get; set; }
		public decimal? VehicleRepairs_Maintenance { get; set; }
		public decimal? Monthly_Subscriptions { get; set; }
		public decimal? ITExpenses { get; set; }
		public decimal? Postage { get; set; }
		public decimal? Telephone { get; set; }
		public decimal? StaffAmenities { get; set; }
		public decimal? ClientEntertainment { get; set; }
		public string ClaimItemStatus { get; set; }
		public DateTime? ClaimApprovedDate { get; set; }
		public string ClaimApprover { get; set; }
		public string Comments { get; set; }

	}
}