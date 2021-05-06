using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class CorporateCardExpenseClaimModel
    {
        public int Id  { get; set; }
        public int  MyProperty { get; set; }
        public DateTime? Date { get; set; }
        public string Supplier { get; set; }
        public string Description { get; set; }
        public string Branch { get; set; }
        public string Project { get; set; }
        public string PONumber{ get; set; }
        public string Travel { get; set; }
        public string Acommodation { get; set; }
        public string Meals { get; set; }
        public string Tools { get; set; }
        public string Other { get; set; }
        public decimal? Total { get; set; }

        
    }
}