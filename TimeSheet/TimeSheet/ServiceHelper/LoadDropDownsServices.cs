using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Models;

namespace TimeSheet.ServiceHelper
{
    public  class LoadDropDownsServices
    {
        public static ProjectionModel ProjectionDropdowns()
        {
            ProjectionModel model = new ProjectionModel();
            model.ProjectList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "Value");
            model.OpportunityNumberList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "OpportunityNumber");
            var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select(x => new ListItemViewModel()
            {
                Id = x.Id,
                Value = x.Value
            });
            int opportunityId = listitem.FirstOrDefault().Id;
            model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
            model.WarehouseList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
            model.ResourceList = new SelectList(ListItemService.Resources().Result, "ID", "Value");
            model.SalesManagerList = new SelectList(ListItemService.SalesManagerList().Result, "ID", "Value");
            model.ProjectmanagerList = new SelectList(ListItemService.ProjectManagerList().Result, "ID", "Value");
            model.ServiceActivityList = new SelectList(ListItemService.ServiceActivitiesList().Result, "ID", "Value");
            model.ProjectionList = ProjectionHelperService.ProjectionListItems().Result;
            model.OpportunityList = ProjectionHelperService.OpportunityListItems().Result;
            return model;
        }
    }
}