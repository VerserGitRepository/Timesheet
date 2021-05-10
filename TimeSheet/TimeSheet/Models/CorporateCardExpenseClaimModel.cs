using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class CorporateCardExpenseClaimModel
    {
		public CorporateCardExpenseClaimModel()
		{
			CorporateCardExpenseClaimItems = new List<CorporateCardExpenseClaimItemsModel>();
		}
		public int Id { get; set; }
		public DateTime ExpenseDateOfClaim { get; set; }
		public string EmployeeName { get; set; }
		public string ApprovedBy { get; set; }
		public string ExpenseClaimType { get; set; }
		public string CreditCardNo { get; set; }
		public string SharePointFileLocation { get; set; }
		public decimal? TravelTotal { get; set; }
		public decimal? MealsTotal { get; set; }
		public decimal? AccommodationTotal { get; set; }
		public decimal? ToolsTotal { get; set; }
		public decimal? OtherTotal { get; set; }
		public decimal ExpenseTotal { get; set; }
		public bool? IsEmailApproved { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? DateUpdated { get; set; }
		public virtual List<CorporateCardExpenseClaimItemsModel> CorporateCardExpenseClaimItems { get; set; }
	}
}