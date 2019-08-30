using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class ManageCalenderController : Controller
    {
        [OutputCache(CacheProfile = "HalfHour", VaryByHeader = "X-Requested-With", Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                TimeSheetViewModel model = new TimeSheetViewModel();
                model.Projectlist = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "Value");
               model.OpportunityNumberList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "OpportunityNumber");
                var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select(x => new ListItemViewModel()
                {
                    Id = x.Id,
                    Value = x.Value
                });
                int opportunityId = listitem.FirstOrDefault().Id;
                model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
                model.WarehouseNameList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
                model.CandidateNameList = new SelectList(ListItemService.Resources().Result, "ID", "Value");
                model.CandidateTimeSheetList = TimeSheetAPIHelperService.TimeSheetList().Result;
                Session["CalenderModel"] = model;
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Index(TimeSheetViewModel SearchModel)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                TimeSheetViewModel model = new TimeSheetViewModel();
                model.Projectlist = new SelectList(TimeSheetAPIHelperService.JMSProjects().Result, "ID", "Value");
                model.OpportunityNumberList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "OpportunityNumber");
                var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select(x => new ListItemViewModel()
                {
                    Id = x.Id,
                    Value = x.Value
                });
                int opportunityId = listitem.FirstOrDefault().Id;
                model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
                model.WarehouseNameList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
                model.CandidateNameList = new SelectList(ListItemService.Resources().Result, "ID", "Value");
                var Search = new SearchViewModel
                {
                    ProjectID = SearchModel.ProjectID,
                    WarehouseNameId = SearchModel.WarehouseID,
                    CandidateNameId = SearchModel.ResourceID,
                    OpportunityNumberID = SearchModel.OpportunityID
                };

                model.CandidateTimeSheetList = SearchFilterService.SearchTimeSheetRecord(Search).Result;
                return View(model);
            }
        }
        [HttpGet]
        public JsonResult GetEvents()
        {
            List<TimeSheetViewModel> model = new List<TimeSheetViewModel>();
            model = TimeSheetAPIHelperService.TimeSheetList().Result;
            model = model.ToList();
            return new JsonResult { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult SearchEvents(string ProjectID, string warehouseId, string opportunityId, string candidateId)
        {
            List<TimeSheetViewModel> model = new List<TimeSheetViewModel>();
            SearchViewModel searchModel = new SearchViewModel()
            {
                CandidateNameId = String.IsNullOrEmpty(candidateId) ? (int?)null : Convert.ToInt32(candidateId),
                WarehouseNameId = String.IsNullOrEmpty(warehouseId) ? (int?)null : Convert.ToInt32(warehouseId),
                OpportunityNumberID = String.IsNullOrEmpty(opportunityId) ? (int?)null : Convert.ToInt32(opportunityId),
                ProjectID = String.IsNullOrEmpty(ProjectID) ? (int?)null : Convert.ToInt32(ProjectID)

            };
            model = SearchFilterService.SearchTimeSheetRecord(searchModel).Result;
            model = model.ToList();
            return new JsonResult { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult SearchResources(string ProjectID, string warehouseId, string opportunityId, List<int> ResourceIDs)
        {
             List<List<TimeSheetViewModel>> modelView = new List<List<TimeSheetViewModel>>();
            #region TO Be Refactored
            // TimeSheetViewModel newModel = new TimeSheetViewModel();
            // //SearchViewModel searchModel = new SearchViewModel()
            // //{
            // //    CandidateNameId = ResourceIDs[0],
            // //    WarehouseNameId = String.IsNullOrEmpty(warehouseId) ? (int?)null : Convert.ToInt32(warehouseId),
            // //    OpportunityNumberID = String.IsNullOrEmpty(opportunityId) ? (int?)null : Convert.ToInt32(opportunityId),
            // //    ProjectID = String.IsNullOrEmpty(ProjectID) ? (int?)null : Convert.ToInt32(ProjectID)

            // //};
            // //modelView = SearchFilterService.SearchTimeSheetRecord(searchModel).Result;
            // TimeSheetViewModel model = (TimeSheetViewModel) Session["CalenderModel"];
            // if (model != null)
            // {
            //     newModel.CandidateTimeSheetList = model.CandidateTimeSheetList.Where(p => p.ProjectID == Convert.ToInt32(ProjectID) && p.WarehouseID == int.Parse(warehouseId) && p.OpportunityID == int.Parse(opportunityId)).ToList<TimeSheetViewModel>();
            //     //model.CandidateTimeSheetList = (from r in model.CandidateTimeSheetList
            //     //                      from bu in ResourceIDs
            //     //                      where bu == r.ResourceID
            //     //                      select r
            //     // ).ToList();

            // }

            // //modelView[0].CandidateTimeSheetList = model.CandidateTimeSheetList;
            // // if(ModelState.
            // newModel.Projectlist = model.Projectlist;
            // newModel.OpportunityNumberList = model.OpportunityNumberList;

            // newModel.ActivityList = model.ActivityList;
            //newModel.WarehouseNameList = model.WarehouseNameList;
            #endregion

            List<TimeSheetViewModel> mergerModel = new List<TimeSheetViewModel>();
            if (ResourceIDs == null)
            {
                SearchViewModel searchModel = new SearchViewModel()
                {
                    
                    WarehouseNameId = String.IsNullOrEmpty(warehouseId) ? (int?)null : Convert.ToInt32(warehouseId),
                    OpportunityNumberID = String.IsNullOrEmpty(opportunityId) ? (int?)null : Convert.ToInt32(opportunityId),
                    ProjectID = String.IsNullOrEmpty(ProjectID) ? (int?)null : Convert.ToInt32(ProjectID)

                };
                List<TimeSheetViewModel> model = SearchFilterService.SearchTimeSheetRecord(searchModel).Result;
                if (model.Count > 0)
                {
                    modelView.Add(model);
                }
            }
            else
            {
                foreach (int itm in ResourceIDs)
                {
                    SearchViewModel searchModel = new SearchViewModel()
                    {
                        CandidateNameId = Convert.ToInt32(itm),
                        WarehouseNameId = String.IsNullOrEmpty(warehouseId) ? (int?)null : Convert.ToInt32(warehouseId),
                        OpportunityNumberID = String.IsNullOrEmpty(opportunityId) ? (int?)null : Convert.ToInt32(opportunityId),
                        ProjectID = String.IsNullOrEmpty(ProjectID) ? (int?)null : Convert.ToInt32(ProjectID)

                    };
                    List<TimeSheetViewModel> model = SearchFilterService.SearchTimeSheetRecord(searchModel).Result;
                    if (model.Count > 0)
                    {
                        modelView.Add(model);
                    }


                }
            }
            foreach (List<TimeSheetViewModel> listModel in modelView)
            {
                foreach (TimeSheetViewModel childModel in listModel)
                {
                    mergerModel.Add(childModel);
                }
            }
            return new JsonResult { Data = mergerModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public JsonResult ProjectOpportunities(int projectId)
        {
            List<ListItemViewModel> load = new List<ListItemViewModel>();
                load = TimeSheetAPIHelperService.ProjectOpportunities(projectId).Result.Select(x => new ListItemViewModel()
                {
                    Id = x.Id,
                    Value = x.Value
                }).ToList();
            
            return Json(load, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ProjectActivities(int OpportunityId)
        {
            List<ListItemViewModel> load = new List<ListItemViewModel>();
            load = TimeSheetAPIHelperService.ProjectActivities(OpportunityId).Result.Select(x => new ListItemViewModel()
            {
                Id = x.Id,
                Value = x.Value
            }).ToList();
            TempData["opportunity"] = OpportunityId;
            return Json(load, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JobNo(int OpportunityId)
        {
            List<ListItemViewModel> Jobs = new List<ListItemViewModel>();
            Jobs = TimeSheetAPIHelperService.JobNo(OpportunityId).Result.Select(x => new ListItemViewModel()
            {
                Id = x.Id,
                Value = x.Value
            }).ToList();
            return Json(Jobs, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Register()
        {
            TimeSheetViewModel model = new TimeSheetViewModel();
            model.Projectlist = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "Value");
            model.OpportunityNumberList = new SelectList(TimeSheetAPIHelperService.CostModelProject().Result, "ID", "OpportunityNumber");
            var listitem = TimeSheetAPIHelperService.CostModelProject().Result.Select(x => new ListItemViewModel()
            {
                Id = x.Id,
                Value = x.Value
            });
            int opportunityId = listitem.FirstOrDefault().Id;
            model.ActivityList = new SelectList(TimeSheetAPIHelperService.ProjectActivities(opportunityId).Result, "ID", "Value");
            model.WarehouseNameList = new SelectList(ListItemService.Warehouses().Result, "ID", "Value");
            model.CandidateNameList = new SelectList(ListItemService.Resources().Result, "ID", "Value");
            model.CandidateTimeSheetList = TimeSheetAPIHelperService.TimeSheetList().Result;
            return PartialView("Register", model);
        }

    }
}