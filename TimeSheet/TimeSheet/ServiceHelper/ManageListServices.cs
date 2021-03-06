﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeSheet.Models;

namespace TimeSheet.ServiceHelper
{
    public class ManageListServices
    {
        public static AdminModel ManageLists()
        {
            AdminModel model = new AdminModel();
            model.OpportunityList = AdminHelperService.OpportunityListItems().Result;
            model.ResourceList = AdminHelperService.ResourceListItems().Result;
            model.ProjectDetailsList = AdminHelperService.ProjectListItems().Result;
            model.ActivityList = AdminHelperService.ActivityListItem().Result;
            model.ProjectManagerList = ListItemService.ProjectManagerList().Result;
            model.SalesManagerList = ListItemService.SalesManagerList().Result;
            model.OpportunitesList = ListItemService.ManageOpportunityModelList().Result;
            model.CorporateCardHolderlist = ExpenseClaimListService.GetExpenseClaims().Result;
            return model;
        }

    }
}