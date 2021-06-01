using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class CorporateCardHolderViewModel
    {
		public int Id { get; set; }
		public string CardHolderName { get; set; }
		public decimal? MonthlyLimit { get; set; }
		public string Branch { get; set; }
		public string CardHolderManager { get; set; }
		public string Bank { get; set; }
		public bool IsActive { get; set; }
	}
}