using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models
{
    public class ProjectDetailsModel
    {
        public List<ProjectDetailsModel> Projects { get; set; }
        public ProjectDetailsModel()
        {
            Projects = new List<ProjectDetailsModel>();
        }
        public int Id { get; set; }         
        public string OpportunityNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }       
        public string SiteAddress { get; set; }      
        public string Project { get; set; }
        public int? ProjectID { get; set; }
        public string Approver { get; set; }    
        public string ProjectManager { get; set; }      
        public string SalesManager { get; set; }      
        public string CustomerContactName { get; set; } 
        public string VerserBranch { get; set; }
        public int? WarehouseNameId { get; set; }
        public int? ResourceID { get; set; }
        public string Activity { get; set; }
        public int? ActivityID { get; set; }
        public int Quantity { get; set; }
        public SelectList WarehouseList { get; set; }       
        public SelectList SalesManagerList { get; set; }
        public SelectList ActivityList { get; set; }
        public SelectList ResourceList { get; set; }
        public SelectList ProjectList { get; set; }
        public SelectList ProjectmanagerList { get; set; }
        public SelectList OpportunityNumberList { get; set; }
        public ProjectionViewModel ProjectionViewModel { get; set; }
        
    }
}