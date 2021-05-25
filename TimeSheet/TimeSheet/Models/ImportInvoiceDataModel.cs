using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ImportInvoiceDataModel
    {
        public int Id { get; set; }
        [Required]
        public string SupplierName { get; set; }
        public string Line { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
       
        public DateTime? ReceivedInvoiceDate { get; set; }
        [Required]
        public decimal? InvoiceAmount_ex_gst { get; set; }
        public decimal? InvoiceAmount_Inc_gst { get; set; }
        public string InvoiceStatus { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Invoice_Date { get; set; }
        [Required]
        public string InvoiceNumber { get; set; }
        public string Project_Job { get; set; }
        [Required]
        public string Approver { get; set; }
        public string Invoice_Description { get; set; }       
        public string PM_Warehouse_HO { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public string ApprovedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ApprovedDate { get; set; }
        public string Comments { get; set; }
        public string Term { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }
        public string POShortage { get; set; }
        public string POShortageComment { get; set; }
        public string GLDescription { get; set; }
        public string GLCode { get; set; }
        public string Branch { get; set; }
        public DateTime? DateRecordedinMYOB { get; set; }
        public string MYOBItemNumber { get; set; }
        public string InvoiceLocation { get; set; }

    }
}