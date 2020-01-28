using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using TimeSheet.Models;
using System.IO;

namespace TimeSheet.Controllers
{
    public class ExcelUtils
    {
        private static Microsoft.Office.Interop.Excel.Workbook mWorkBook;
        private static Microsoft.Office.Interop.Excel.Sheets mWorkSheets;
        private static Microsoft.Office.Interop.Excel.Worksheet mWSheet1;
        private static Microsoft.Office.Interop.Excel.Application oXL;

       

        public static void openExcel(IEnumerable<CompletedTimesheetModel> model, string thePath)
        {
            string sourcefile = thePath + "\\TimeScheduler.xls";
            string dest = thePath + "\\ApprovedList.xls";
            File.Copy(sourcefile, dest);
            string path = dest;
            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = false;
            oXL.DisplayAlerts = false;
            mWorkBook = oXL.Workbooks.Open(path, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            //Get all the sheets in the workbook
            mWorkSheets = mWorkBook.Worksheets;
            //Get the allready exists sheet
            mWSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)mWorkSheets.get_Item(5);
            Microsoft.Office.Interop.Excel.Range range = mWSheet1.UsedRange;
            int colCount = range.Columns.Count;
            int rowCount = range.Rows.Count;
            //for (int index = 1; index < 15; index++)
            //{
            //    mWSheet1.Cells[rowCount + index, 1] = rowCount + index;
            //    mWSheet1.Cells[rowCount + index, 2] = "New Item" + index;
            //}
            int index = 1;
            foreach (CompletedTimesheetModel item in model)
            {
                
                mWSheet1.Cells[rowCount + index, 1] = index;
                mWSheet1.Cells[rowCount + index, 2] = index;
                mWSheet1.Cells[rowCount + index, 3] = item.AdpEmployeeID;
                mWSheet1.Cells[rowCount + index, 4] = item.CandidateName;
                mWSheet1.Cells[rowCount + index, 5] = item.Day;
                mWSheet1.Cells[rowCount + index, 6] = item.StartTime;
                mWSheet1.Cells[rowCount + index, 7] = item.EndTime;
                mWSheet1.Cells[rowCount + index, 8] = item.ProjectName;
                mWSheet1.Cells[rowCount + index, 9] = item.Activity;
                mWSheet1.Cells[rowCount + index, 10] = item.EndTime.Value.Subtract(item.StartTime.Value).TotalMinutes / 60;
                mWSheet1.Cells[rowCount + index, 11] = item.OLATarget;
                mWSheet1.Cells[rowCount + index, 12] = item.OLATarget;
                mWSheet1.Cells[rowCount + index, 13] = item.ActualQuantity;
                mWSheet1.Cells[rowCount + index, 14] = item.ActualQuantity;//variance
                mWSheet1.Cells[rowCount + index, 15] = item.ProjectManager;
                mWSheet1.Cells[rowCount + index, 16] = item.JobNo;
                mWSheet1.Cells[rowCount + index, 17] = item.OpportunityID;
                mWSheet1.Cells[rowCount + index, 18] = item.Day.Value.DayOfWeek;
                mWSheet1.Cells[rowCount + index, 19] = item.WarehouseName;
                mWSheet1.Cells[rowCount + index, 20] = item.TimeSheetComments;
                mWSheet1.Cells[rowCount + index, 21] = item.EmploymentTypeID;
                index++;
                
            }
            mWorkBook.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
            Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
            Missing.Value, Missing.Value, Missing.Value,
            Missing.Value, Missing.Value);
            mWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
            mWSheet1 = null;
            mWorkBook = null;
            oXL.Quit();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();


           
        }
      

        
    }
}