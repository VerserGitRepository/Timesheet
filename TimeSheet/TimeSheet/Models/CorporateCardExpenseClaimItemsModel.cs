using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class CorporateCardExpenseClaimItemsModel
    {
		public int Id { get; set; }
		public int? Fk_CorporateCardExpenseClaimID { get; set; }
		public DateTime? ClaimDate { get; set; }
		public string Supplier { get; set; }
		public string ItemDescription { get; set; }
		public string Project { get; set; }
		public string Branch { get; set; }
		public string ClaimItemStatus { get; set; }
		public string PONumber { get; set; }
		public decimal? TravelCost { get; set; }
		public decimal? MealsCost { get; set; }
		public decimal? AccommodationCost { get; set; }
		public decimal? ToolsCost { get; set; }
		public decimal? WHTools_Consumables { get; set; }
		public decimal? VehicleRepairs_Maintenance { get; set; }
		public decimal? Monthly_Subscriptions { get; set; }
		public decimal? ITExpenses { get; set; }
		public decimal? Postage { get; set; }
		public decimal? Telephone { get; set; }
		public decimal? StaffAmenities { get; set; }
		public decimal? ClientEntertainment { get; set; }
		public decimal? OtherCost { get; set; }
		public decimal? ExpenseTotal { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? DateUpdated { get; set; }
	}
}