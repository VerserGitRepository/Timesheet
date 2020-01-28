using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeSheet.Models
{
    public class AdminModel
    {
        public List<OpportunityListModel> OpportunityList { get; set; }
        public List<ProjectListModel> ProjectDetailsList { get; set; }
        public List<ResourceListModel> ResourceList { get; set; }
        public List<ActivityListModel> ActivityList { get; set; }
        public List<ListItemViewModel> ProjectManagerList { get; set; }
        public List<ListItemViewModel> SalesManagerList { get; set; }
        public List<OpportunityListItem> OpportunitesList { get; set; }
    }
}
