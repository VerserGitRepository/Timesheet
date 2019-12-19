using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;

namespace TimeSheet.Controllers
{
    public class ProfitLossController : Controller
    {
        // GET: ProfitLoss
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ProfitLossModel model = new ProfitLossModel();
                model.ProfitLossList = TimeSheetAPIHelperService.ProfitLoss().Result;
                // model.HasUserPermissionsToEdit = UserRoles.UserCanEditTimesheet();
                var groupedPLModel = model.ProfitLossList.GroupBy(x => new { x.opportunityNumber});
                List<AggregatedProfitLossModel> agrProfitLossModel = new List<AggregatedProfitLossModel>();
                foreach (var groupInfo in groupedPLModel)
                {
                    AggregatedProfitLossModel themodel = new AggregatedProfitLossModel();
                    foreach (var subGroup in groupInfo)
                    {
                        themodel.opportunityNumber = subGroup.opportunityNumber;
                        themodel.serviceDescription = subGroup.serviceDescription;
                        themodel.projectName = subGroup.projectName;
                        themodel.CostModeltotalCost += Convert.ToDouble(subGroup.costmodelquantity) * Convert.ToDouble(subGroup.totalCost);
                        themodel.CostModeltotalProfit += Convert.ToDouble(subGroup.costmodelquantity) * Convert.ToDouble(subGroup.totalProfit);

                        themodel.ProjectiontotalCost += Convert.ToDouble(subGroup.projectionquantity) * Convert.ToDouble(subGroup.totalCost);
                        themodel.ProjectiontotalProfit += Convert.ToDouble(subGroup.projectionquantity) * Convert.ToDouble(subGroup.totalProfit);

                        themodel.costmodelquantity += subGroup.costmodelquantity;
                        themodel.projectionquantity += Convert.ToDouble(subGroup.projectionquantity);
                        themodel.GAP += Convert.ToDouble(subGroup.projectionquantity) < Convert.ToDouble(subGroup.costmodelquantity) ? Convert.ToDouble(subGroup.projectionquantity) * Convert.ToDouble(subGroup.totalProfit) - Convert.ToDouble(subGroup.costmodelquantity) * Convert.ToDouble(subGroup.totalProfit) : 0;
                        themodel.Profit += Convert.ToDouble(subGroup.projectionquantity) > Convert.ToDouble(subGroup.costmodelquantity) ? Convert.ToDouble(subGroup.projectionquantity) * Convert.ToDouble(subGroup.totalProfit) - Convert.ToDouble(subGroup.costmodelquantity) * Convert.ToDouble(subGroup.totalProfit) : 0;

                    }
                    agrProfitLossModel.Add(themodel);

                }
                model.AggregatedProfitLossModel = agrProfitLossModel;
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult CostDetails(string opportunityNumber)
        {
            ProfitLossModel model = new ProfitLossModel();
            var AlltimesheetRecords = TimeSheetAPIHelperService.ProfitLoss().Result;
            model.ProfitLossList = AlltimesheetRecords.Where(c => c.opportunityNumber == opportunityNumber).ToList();
           
            return PartialView("CostDetails", model);
        }
        [HttpPost]
        public ActionResult ExportPandL()
        {
            ProfitLossModel model = new ProfitLossModel();
           

            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                model.ProfitLossList = TimeSheetAPIHelperService.ProfitLoss().Result;
                
                GridView gv = new GridView();
                gv.DataSource = model.ProfitLossList;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=ProfitAndLossExport.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            return RedirectToAction("Index", "ProfitLoss");
        }
    }
}