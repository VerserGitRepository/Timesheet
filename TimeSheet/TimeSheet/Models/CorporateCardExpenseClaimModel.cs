﻿using System;
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
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime ExpenseDateOfClaim { get; set; }
		[Required]
		public string EmployeeName { get; set; }
		[Required]
		public string ApprovedBy { get; set; }
		[Required]
		public string ExpenseClaimType { get; set; }	
		[Required]
		public string SharePointFileLocation { get; set; }
		public decimal? WHTools_ConsumablesTotal { get; set; }
		public decimal? VehicleRepairs_MaintenanceTotal { get; set; }
		public decimal? Monthly_SubscriptionsTotal { get; set; }
		public decimal? ITExpensesTotal { get; set; }
		public decimal? PostageTotal { get; set; }
		public decimal? TelephoneTotal { get; set; }
		public decimal? StaffAmenitiesTotal { get; set; }
		public decimal? ClientEntertainmentTotal { get; set; }
		public decimal? TravelTotal { get; set; }
		public decimal? MealsTotal { get; set; }
		public decimal? AccommodationTotal { get; set; }	
		public decimal? ToolsTotal { get; set; }
		public string ClaimStatus { get; set; }		
		public decimal? OtherTotal { get; set; }	
		public decimal ExpenseTotal { get; set; }
		public bool? IsEmailApproved { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? DateUpdated { get; set; }
        public string CreatedBy { get; set; }
		public string ClaimApprover { get; set; }
		public virtual List<CorporateCardExpenseClaimItemsModel> CorporateCardExpenseClaimItems { get; set; }
	}
}