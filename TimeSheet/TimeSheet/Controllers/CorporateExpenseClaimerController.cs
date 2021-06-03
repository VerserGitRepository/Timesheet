using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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

        public ActionResult ExpenseClaimItems()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ExpenseClaimItems(int ClaimId)
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            var ClaimItemsData = ExpenseClaimerService.GetExpenseClaimItemsById(ClaimId).Result;
            return View(ClaimItemsData);
        }
        public ActionResult SupplierInvoiceItems()
        {
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult SupplierInvoiceItems(int InvoiceId)
        {
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
        public ActionResult ApproveExpenseClaimItem(int ClaimItemId, string claimItemStatus)
        {
            ExpenseClaimerService.ApproveExpenseClaimItem(ClaimItemId, claimItemStatus);
            return RedirectToAction("ExpenseClaims", "CorporateExpenseClaimer");
        }

        //ApproveExpenseClaimItem
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
        public ActionResult CreateExpenseClaimItemEntity(CorporateCardExpenseClaimModel ClaimItemsUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ExpenseClaims", "CorporateExpenseClaimer");
            }
            if (!UserRoles.IsLoginActive())
            {
                return RedirectToAction("Login", "Login");
            }
            ExpenseClaimerService.RegisterExpenseClaim(ClaimItemsUpdateModel);
            return RedirectToAction("ExpenseClaims", "CorporateExpenseClaimer");
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

        //[HttpPost]
        //public ActionResult ImportInvoices()
        //{
        //    try
        //    {             

        //        HttpPostedFileBase file = Request.Files["file"];
        //        if (file.ContentLength > 0)
        //        {
        //            string _FileName = Path.GetFileName(file.FileName);
        //            string _path = Path.Combine(Server.MapPath("~/InvoiceExcelFiles"), _FileName);
        //            file.SaveAs(_path);
        //            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(_path, false))
        //            {
        //                WorkbookPart wbPart = doc.WorkbookPart;
        //                Sheet mysheet = (Sheet)doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);
        //                Worksheet Worksheet = ((WorksheetPart)wbPart.GetPartById(mysheet.Id)).Worksheet;

        //                //Note: worksheet has 8 children and the first child[1] = sheetviewdimension,....child[4]=sheetdata  
        //                int wkschildno = 4;
        //                SheetData Rows = (SheetData)Worksheet.ChildElements.GetItem(wkschildno);
        //                var assetList = new List<InvoiceViewModel>();
        //                for (int row = 1; row < Rows.Count(); row++)
        //                {
        //                    Row currentrow = (Row)Rows.ChildElements.GetItem(row);
        //                    if (!string.IsNullOrEmpty(GetCellValue((Cell)currentrow.ChildElements.GetItem(0), wbPart)))
        //                    {
        //                        CultureInfo provider = CultureInfo.InvariantCulture;                             

        //                        var asset = new InvoiceViewModel();
        //                        asset.SupplierName = GetCellValue((Cell)currentrow.ChildElements.GetItem(0), wbPart);
        //                        asset.Line = GetCellValue((Cell)currentrow.ChildElements.GetItem(1), wbPart);
        //                        string _ReceivedInvoiceDate = GetCellValue((Cell)currentrow.ChildElements.GetItem(2), wbPart);
        //                        DateTime dateTime16 = DateTime.ParseExact(_ReceivedInvoiceDate, new string[] {"dd/MM/yyyy" }, provider, DateTimeStyles.None);
        //                        asset.ReceivedInvoiceDate = dateTime16;// Convert.ToDateTime();
        //                        asset.InvoiceAmount_ex_gst =Convert.ToDecimal( GetCellValue((Cell)currentrow.ChildElements.GetItem(3), wbPart));
        //                        string _Invoice_Date = GetCellValue((Cell)currentrow.ChildElements.GetItem(4), wbPart);
        //                        DateTime dateTime17 = DateTime.ParseExact(_Invoice_Date, new string[] {"dd/MM/yyyy" }, provider, DateTimeStyles.None);
        //                        asset.Invoice_Date = dateTime17;// Convert.ToDateTime(GetCellValue((Cell)currentrow.ChildElements.GetItem(4), wbPart));
        //                        asset.InvoiceNumber = GetCellValue((Cell)currentrow.ChildElements.GetItem(5), wbPart);
        //                        asset.Project_Job = GetCellValue((Cell)currentrow.ChildElements.GetItem(6), wbPart);
        //                        asset.Approver = GetCellValue((Cell)currentrow.ChildElements.GetItem(7), wbPart);
        //                        asset.Invoice_Description = GetCellValue((Cell)currentrow.ChildElements.GetItem(8), wbPart);
        //                        asset.PM_Warehouse_HO = GetCellValue((Cell)currentrow.ChildElements.GetItem(9), wbPart);
        //                        asset.Comments= GetCellValue((Cell)currentrow.ChildElements.GetItem(10), wbPart);
        //                        asset.Term = GetCellValue((Cell)currentrow.ChildElements.GetItem(11), wbPart);
        //                        string _DueDate= GetCellValue((Cell)currentrow.ChildElements.GetItem(12), wbPart);
        //                        DateTime dateTime12 = DateTime.ParseExact(_DueDate, new string[] { "dd/MM/yyyy" }, provider, DateTimeStyles.None);
        //                        asset.DueDate = dateTime12;
        //                        asset.POShortage = GetCellValue((Cell)currentrow.ChildElements.GetItem(13), wbPart);
        //                        asset.POShortageComment = GetCellValue((Cell)currentrow.ChildElements.GetItem(14), wbPart);
        //                        asset.GLDescription = GetCellValue((Cell)currentrow.ChildElements.GetItem(15), wbPart);
        //                        asset.GLCode = GetCellValue((Cell)currentrow.ChildElements.GetItem(16), wbPart);
        //                        asset.Branch = GetCellValue((Cell)currentrow.ChildElements.GetItem(17), wbPart);
        //                        asset.MYOBItemNumber = GetCellValue((Cell)currentrow.ChildElements.GetItem(18), wbPart);
        //                        assetList.Add(asset);
        //                      //  Approved / Disputed Status Comments(for internal purpose)	Term Due Date PO Shortage PO Shortage 
        //                        //Comment GL Description  GL code Branch Date Recorded in MYOB MYOB Item Number
        //                    }
        //                    else
        //                    {
        //                        break;
        //                    }
        //                }

        //                 ImportInvoiceDataService.AddImportedInvoiceLines(assetList);                        
        //            }
        //            //Delete the file after reading
        //            // System.IO.File.Delete(_path);
        //        }
        //        else
        //        {
        //            System.Web.HttpContext.Current.Session["ErrorMessage"] = "File Not choosen Please a file";
        //        }
        //    }
        //    catch (System.Exception Ex)
        //    {
        //        System.Web.HttpContext.Current.Session["ErrorMessage"] = Ex.Message;
        //    }
        //    return RedirectToAction("InvoiceApprovals", "CorporateExpenseClaimer");
        //}

        [HttpPost]
        public ActionResult CreateNewInvoiceLine(InvoiceViewModel _ImportInvoiceDataModel)
        {
            // var json = Newtonsoft.Json.JsonConvert.SerializeObject(_ImportInvoiceDataModel);
            ImportInvoiceDataService.CreateInvoiceItem(_ImportInvoiceDataModel);
            return RedirectToAction("InvoiceApprovals", "CorporateExpenseClaimer");
        }

        [HttpGet]
        public ActionResult CreateNewInvoiceLine()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InvoiceApprovals()
        {
            //<ImportInvoiceDataModel>> GetExpenseClaims()
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
            ImportInvoiceDataService.ApproveExpenseInvoice(InvoiceItemId);
            return RedirectToAction("InvoiceApprovals", "CorporateExpenseClaimer");
        }
        [HttpPost]
        public ActionResult UnApproveSupplierInvoiceItem(int InvoiceItemId)
        {
            ImportInvoiceDataService.DisputedExpenseInvoice(InvoiceItemId);
            return RedirectToAction("InvoiceApprovals", "CorporateExpenseClaimer");
        }

        //ApproveSupplierInvoiceItem(int InvoiceItemId)

        // UnApproveSupplierInvoiceItem(int InvoiceItemId)
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
    }
}