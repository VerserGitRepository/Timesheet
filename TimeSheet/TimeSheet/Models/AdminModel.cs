﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class AdminModel
    {
        public List<OpportunityListModel> OpportunityList { get; set; }
        public List<ProjectListModel> ProjectDetailsList { get; set; }
        public List<ResourceListModel> ResourceList { get; set; }
        public List<ActivityListModel> ActivityList { get; set; }
    }
}