using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel()
        {
            InvoiceLineItems = new List<InvoiceLineItemsViewModel>();
        }
        public int Id { get; set; }
        [Required]
        public string SupplierName { get; set; }   
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]       
        public DateTime? ReceivedInvoiceDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Invoice_Date { get; set; }
        [Required]
        public string InvoiceNumber { get; set; }
        public string Term { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }

        public List<InvoiceLineItemsViewModel> InvoiceLineItems { get; set; }

    }
}