using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TimeSheet.Models
{
    public class ProjectionModel
    {
        public List<ProjectionModel> ProjectionList { get; set; }    
        public ProjectionModel()
        {
            ProjectionList = new List<ProjectionModel>();
           
        }
        public int Id { get; set; }
        public int ProjectionId { get; set; }
        public string OpportunityNumber { get; set; }
        public int ActivityId { get; set; }
        public bool? IsActive { get; set; }
        public int? ServiceActivityId { get; set; }      
        public string ProjectManager { get; set; }    
        public int ProjectID { get; set; }
        public string VerserBranch { get; set; }
        public string Activity { get; set; }
        public int WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public int OpportunityID { get; set; }    
        public int ProjectManagerID { get; set; }      
        public int Quantity { get; set; }
        public int ProjectionQuantity { get; set; }
        public int CostModelQuantity { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Created { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateInvoiced { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateModified { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateAllocated { get; set; }

        public SelectList WarehouseList { get; set; }
        public SelectList SalesManagerList { get; set; }
        public SelectList ActivityList { get; set; }
        public SelectList ServiceActivityList { get; set; }
        public SelectList ResourceList { get; set; }   
        public SelectList ProjectList { get; set; }
        public SelectList ProjectmanagerList { get; set; }
        public SelectList OpportunityNumberList { get; set; }
        public ProjectionViewModel ProjectionViewModel { get; set; }      
        public List<ProjectionOppurtunityModel> projectionOpportunityModel { get; set; }
        public string SiteAddress { get; set; }
        public string Comments { get; set; }
        public string ProjectName { get; set; }
        public string PriceperUnit { get; set; }
        public double PriceperUnitTotal => Convert.ToDouble(Quantity) * Convert.ToDouble(PriceperUnit);
        public List<OpportunityListModel> OpportunityList { get; set; }
        public bool IsAdd { get; set; }
        public int ServiceRevenueId { get; set; }

    }
}