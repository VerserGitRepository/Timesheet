using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class JMSProjectsViewModel
    {
        public int Id { get; set; }
        public string ProjectType { get; set; }
        public string Summary { get; set; }
        public string ClientReference { get; set; }
        public int MYOBCostCentre_CostCentreID { get; set; }
        public int MYOBEmployee_EmployeeID { get; set; }
        public int MYOBEmployee_EmployeeID1 { get; set; }
        public int? MYOBJob_JobID { get; set; }
        public int? MYOBSupplier_SupplierID { get; set; }
    }
}