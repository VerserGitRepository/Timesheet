using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class EstimatedTravelCostModel
    {
        public int Id { get; set; }
        public decimal BookingTravelCost { get; set; }
        public decimal BookingTravelHours { get; set; }
        public int CalculateOLA { get; set; }
    }
}