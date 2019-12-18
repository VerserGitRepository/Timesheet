using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models
{
    public class AggregatedProfitLossModel
    {
       
        public string opportunityNumber { get; set; }
       
        public string projectName { get; set; }
       
        public decimal costPerUnit { get; set; }
       
        public double CostModeltotalCost { get; set; }       
        public double CostModeltotalProfit { get; set; }
        public double ProjectiontotalCost { get; set; }
        public double ProjectiontotalProfit { get; set; }

        public string serviceDescription { get; set; }
      
        public double costmodelquantity { get; set; }
        public double projectionquantity { get; set; }

        public double GAP { get; set; }
        public double Profit { get; set; }



    }
}