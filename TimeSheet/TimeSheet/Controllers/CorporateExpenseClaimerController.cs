﻿using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheet.Models;
using TimeSheet.ServiceHelper;


namespace TimeSheet.Controllers
{
    public class CorporateExpenseClaimerController : Controller
    {
        public ActionResult Index()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        public ActionResult ExpenseClaims()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            var ClaimData = ExpenseClaimerService.GetExpenseClaims().Result;
            return View(ClaimData); 
        }
        public ActionResult ExpenseClaimItems(int? ClaimId)
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            if (ClaimId > 0)
            {
                var ClaimItemsData = ExpenseClaimerService.GetExpenseClaimItemsById(Convert.ToInt32(ClaimId)).Result;
                return View(ClaimItemsData);
            }
            return View();
        }
        [HttpPost]
        public ActionResult ExpenseClaimItems(int ClaimId)
        {
            Session["ClaimId"] = ClaimId;
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            var ClaimItemsData = ExpenseClaimerService.GetExpenseClaimItemsById(ClaimId).Result;
            return View(ClaimItemsData);
        }
        public ActionResult SupplierInvoiceItems(int? InvoiceId)
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            if (InvoiceId >0)
            {
                var ClaimItemsData = ExpenseClaimerService.GetSupplierInvoiceItemsById(Convert.ToInt32(InvoiceId)).Result;
                return View(ClaimItemsData);
            }
            return View();
        }
        [HttpPost]
        public ActionResult SupplierInvoiceItems(int InvoiceId)
        {
            Session["InvoiceId"]= InvoiceId;
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            var ClaimItemsData = ExpenseClaimerService.GetSupplierInvoiceItemsById(InvoiceId).Result;
            return View(ClaimItemsData);
        }
        [HttpPost]
        public ActionResult UpdateExpenseClaimItem(int ClaimItemId)
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            var ClaimItemsData = ExpenseClaimerService.GetExpenseClaimItem(ClaimItemId).Result;
            return View(ClaimItemsData);
        }

        [HttpPost]
        public ActionResult ApproveExpenseClaimItem(int ClaimItemId, string claimItemStatus,string unapprovalComments)
        {
            string ClaimApprover = UserRoles.GetLoggedInActiveUser();          
            var ClaimId = Session["ClaimId"];
            if (ClaimId != null && ClaimApprover !=null)
            {
                if (string.IsNullOrEmpty(unapprovalComments))
                {
                    unapprovalComments = "Approved";
                }
                ExpenseClaimerService.ApproveExpenseClaimItem(ClaimItemId, claimItemStatus, ClaimApprover, unapprovalComments);
                return RedirectToAction("ExpenseClaimItems", "CorporateExpenseClaimer", new { ClaimId = Convert.ToInt32(ClaimId) });
            }
            else
            {
                return RedirectToAction("ExpenseClaims", "CorporateExpenseClaimer");
            }           
        }
        [HttpPost]
        public ActionResult UpdateExpenseClaimItemEntity(CorporateCardExpenseClaimItemsModel ClaimItemsUpdateModel)
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            ClaimItemsUpdateModel.ExpenseTotal = (ClaimItemsUpdateModel.AccommodationCost + ClaimItemsUpdateModel.TravelCost + ClaimItemsUpdateModel.ToolsCost + ClaimItemsUpdateModel.MealsCost + ClaimItemsUpdateModel.OtherCost);
            ExpenseClaimerService.UpdateExpenseClaimItem(ClaimItemsUpdateModel);
            return RedirectToAction("ExpenseClaims", "CorporateExpenseClaimer");
        }

        [HttpPost]
        public JsonResult CreateExpenseClaimItemEntity(CorporateCardExpenseClaimModel ClaimItemsUpdateModel)
        {
            bool returnvalue = false;
            if (UserRoles.IsLoginActive() && ModelState.IsValid)
            {
                ClaimItemsUpdateModel.CreatedBy = UserRoles.GetLoggedInActiveUser();                
                returnvalue = ExpenseClaimerService.RegisterExpenseClaim(ClaimItemsUpdateModel);     
            }           
            return Json(returnvalue, JsonRequestBehavior.AllowGet);            
        }
        [HttpPost]
        public ActionResult ExportExpenseClaims()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }

            GridView gv = new GridView();
            gv.DataSource = ExpenseClaimerService.ExportExpenseClaims().Result; ;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", $"attachment; filename=ExpenseClaims-{DateTime.Now.ToShortDateString()}.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("ExpenseClaims", " CorporateExpenseClaimer");
        }

        [HttpPost]
        public ActionResult ExportInvoiceItems()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }

            GridView gv = new GridView();
            gv.DataSource = ImportInvoiceDataService.GetInvoiceLineItems().Result;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", $"attachment; filename=ExpenseClaims-{DateTime.Now.ToShortDateString()}.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("ExpenseClaims", " CorporateExpenseClaimer");
        }

        [HttpPost]
        public JsonResult CreateNewInvoiceLine(InvoiceViewModel _ImportInvoiceDataModel)
        {            
            var returnvalue=ImportInvoiceDataService.CreateInvoiceItem(_ImportInvoiceDataModel);
            return Json(returnvalue, JsonRequestBehavior.AllowGet);        
        }

        [HttpGet]
        public ActionResult CreateNewInvoiceLine()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult InvoiceApprovals()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            var Invoices = ImportInvoiceDataService.GetInvoiceLineItems().Result;
            return View(Invoices);
        }
        [HttpPost]
        public ActionResult ApproveInvoice(int InvoiceId)
        {
            ImportInvoiceDataService.ApproveExpenseInvoice(InvoiceId);
            return RedirectToAction("InvoiceApprovals", "CorporateExpenseClaimer");
        }
        [HttpPost]
        public ActionResult UpdateInvoicePaid(int InvoiceId)
        {
            ImportInvoiceDataService.UpdateInvoicePaid(InvoiceId);
            return RedirectToAction("InvoiceApprovedItems", "CorporateExpenseClaimer");
        }
        [HttpPost]
        public ActionResult DisputedInvoice(int InvoiceId)
        {
            ImportInvoiceDataService.DisputedExpenseInvoice(InvoiceId);
            return RedirectToAction("InvoiceApprovals", "CorporateExpenseClaimer");
        }
        public ActionResult UpdateInvoiceLine()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult UpdateInvoiceLine(int InvoiceItemId)
        {
            var UpdatebleItem = ImportInvoiceDataService.GetInvoiceLineItem(InvoiceItemId).Result;
            return View(UpdatebleItem);
        }
        [HttpPost]
        public ActionResult UpdateInvoiceLineItem(InvoiceViewModel InvoiceLineItemModel)
        {
            ImportInvoiceDataService.UpdateInvoiceLineItem(InvoiceLineItemModel);
            return RedirectToAction("InvoiceApprovals", "CorporateExpenseClaimer");
        }
        private static string GetCellValue(Cell theCell, WorkbookPart wbPart)
        {
            string value = "";
            if (theCell != null)
            {
                value = theCell.InnerText;

                // If the cell represents an integer number, you are done. 
                // For dates, this code returns the serialized value that 
                // represents the date. The code handles strings and 
                // Booleans individually. For shared strings, the code 
                // looks up the corresponding value in the shared string 
                // table. For Booleans, the code converts the value into 
                // the words TRUE or FALSE.
                if (theCell.DataType != null)
                {
                    switch (theCell.DataType.Value)
                    {
                        case CellValues.SharedString:

                            // For shared strings, look up the value in the
                            // shared strings table.
                            var stringTable =
                                wbPart.GetPartsOfType<SharedStringTablePart>()
                                .FirstOrDefault();

                            // If the shared string table is missing, something 
                            // is wrong. Return the index that is in
                            // the cell. Otherwise, look up the correct text in 
                            // the table.
                            if (stringTable != null)
                            {
                                value =
                                    stringTable.SharedStringTable
                                    .ElementAt(int.Parse(value)).InnerText;
                            }
                            break;

                        case CellValues.Boolean:
                            switch (value)
                            {
                                case "0":
                                    value = "FALSE";
                                    break;
                                default:
                                    value = "TRUE";
                                    break;
                            }
                            break;
                    }
                }
            }
            return value;
        }

        [HttpGet]
        public ActionResult InvoiceApprovedItems()
        {
            var Invoices = ImportInvoiceDataService.GetApprovedInvoiceLineItems().Result;
            return View(Invoices);
        }

        [HttpPost]
        public ActionResult ApproveSupplierInvoiceItem(int InvoiceItemId)
        {
            ImportInvoiceDataService.ApproveSupplierInvoiceItem(true,InvoiceItemId);

            var InvoiceId = Session["InvoiceId"];
            if (InvoiceId != null)
            {
                return RedirectToAction("SupplierInvoiceItems", "CorporateExpenseClaimer",new {invoiceId= Convert.ToInt32(InvoiceId) });
               
            }
            else
            {               
               return RedirectToAction("InvoiceApprovals", "CorporateExpenseClaimer");
            }           
        }
        [HttpPost]
        public ActionResult UnApproveSupplierInvoiceItem(int InvoiceItemId)
        {
            ImportInvoiceDataService.ApproveSupplierInvoiceItem(false,InvoiceItemId);
            var InvoiceId = Session["InvoiceId"];
            if (InvoiceId != null)
            {
                return RedirectToAction("SupplierInvoiceItems", "CorporateExpenseClaimer", new { invoiceId = Convert.ToInt32(InvoiceId) });

            }
            else
            {
                return RedirectToAction("InvoiceApprovals", "CorporateExpenseClaimer");
            }
        }
        [HttpPost]
        public ActionResult ExportApprovedInvoices()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }

            GridView gv = new GridView();
            gv.DataSource = ImportInvoiceDataService.GetApprovedInvoiceLineItems().Result;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", $"attachment; filename=ExpenseClaims-{DateTime.Now.ToShortDateString()}.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("ExpenseClaims", " CorporateExpenseClaimer");
        }
        [HttpPost]
        public ActionResult ExportAllInvoices()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }

            GridView gv = new GridView();
            gv.DataSource = ImportInvoiceDataService.ExportAllInvoices().Result;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", $"attachment; filename=ExpenseClaims-{DateTime.Now.ToShortDateString()}.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("ExpenseClaims", " CorporateExpenseClaimer");
        }

        [HttpGet]
        public ActionResult GetSuppliers()
        {
            var _ItemTypes = ListItemService.GetSuppliers().Result;
            return Json(_ItemTypes, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetGLCodes()
        {
            var _ItemTypes = ListItemService.GetSuppliersGLCodes().Result;
            return Json(_ItemTypes, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetCardHolders()
        {
            var _ItemTypes = ListItemService.Resources().Result;// ListItemService.GetCardHolders().Result;
            return Json(_ItemTypes, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetResourceList()
        {
            var _ItemTypes = ListItemService.Resources().Result;
            return Json(_ItemTypes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProjectList()
        {
            var _ItemTypes = TimeSheetAPIHelperService.CostModelProject().Result;
            return Json(_ItemTypes, JsonRequestBehavior.AllowGet);
        }
    }

}