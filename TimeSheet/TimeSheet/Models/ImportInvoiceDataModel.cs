using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ImportInvoiceDataModel
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string Line { get; set; }
        public DateTime? ReceivedInvoiceDate { get; set; }
        public decimal? InvoiceAmount_ex_gst { get; set; }
        public decimal? InvoiceAmount_Inc_gst { get; set; }
        public DateTime? Invoice_Date { get; set; }
        public string InvoiceNumber { get; set; }
        public string Project_Job { get; set; }
        public string Approver { get; set; }
        public string Invoice_Description { get; set; }       
        public string PM_Warehouse_HO { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}