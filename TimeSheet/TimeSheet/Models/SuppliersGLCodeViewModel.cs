using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class SuppliersGLCodeViewModel
    {
        public int Id { get; set; }
        public int Fk_InvoiceSupplierID { get; set; }
        public string GLDescription { get; set; }
        public string GLNumber { get; set; }
        public bool? IsActive { get; set; }
    }
}