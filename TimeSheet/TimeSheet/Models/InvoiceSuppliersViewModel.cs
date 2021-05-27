using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class InvoiceSuppliersViewModel
    {
		public int Id { get; set; }
		public string SupplierName { get; set; }
		public int Term { get; set; }
		public string Branch { get; set; }
		public string GLDescription { get; set; }
		public bool IsActive { get; set; }
	}
}