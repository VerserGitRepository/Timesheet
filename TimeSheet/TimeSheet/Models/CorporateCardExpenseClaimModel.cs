using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		[Required]
		public DateTime ExpenseDateOfClaim { get; set; }
		[Required]
		public string EmployeeName { get; set; }
		[Required]
		public string ApprovedBy { get; set; }
		[Required]
		public string ExpenseClaimType { get; set; }
		[Required]
		public string CreditCardNo { get; set; }
		[Required]
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