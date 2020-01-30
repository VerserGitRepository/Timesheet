using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TimeSheet.Models;

namespace TimeSheet.ServiceHelper
{
    public class RegisterTimesheetService
    {
        private static readonly string TimeSheetAPIURl = ConfigurationManager.AppSettings["TimeSheetBaseURL"] + ConfigurationManager.AppSettings["TimeSheetRootDirectory"];
        public static async Task<ReturnModel> RegisterTimesheetModel(TimeSheetRegisterModel RegisterModel)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(TimeSheetAPIURl);
                    HttpResponseMessage response = client.PostAsJsonAsync(string.Format("TimeSheet/Register"), RegisterModel).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                        if (ReturnResult.Message == null || ReturnResult.Message == string.Empty)
                        {
                            ReturnResult.Message = "Candidate Timesheet has been registered successfully.";
                            HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                        }
                        else if (ReturnResult.Message.Contains("Is Already Booked"))
                        {
                           // ReturnResult.Message = "Candidate Timesheet has been registered successfully.";
                            HttpContext.Current.Session["InfoMessage"] = ReturnResult.Message;
                        }
                        else
                        {
                            HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                        }
                    }
                    else
                    {
                        ReturnResult.Message = "There is an issue with the booking. The detail are " + response.ReasonPhrase;
                        HttpContext.Current.Session["ErrorMessage"] = ReturnResult.Message;
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Session["ErrorMessage"] = "There is an issue with the booking. The detail are " + ex.Message;
                }
            }
            return ReturnResult; 
        }

        public static async Task<ReturnModel> EditTimesheetModel(UpdateTimeSheetModel UpdateModel)
        {
            ReturnModel ReturnResult = new ReturnModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("TimeSheet/UpdateTimeSheet"), UpdateModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                    HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = ReturnResult.Message;
                }
            }
            return ReturnResult; 
        }
        //public static async Task<ReturnModel> UpdateBreakHours(UpdateTimeSheetModel UpdateModel)
        //{
        //    ReturnModel ReturnResult = new ReturnModel();

        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(TimeSheetAPIURl);
        //        HttpResponseMessage response = client.GetAsync(string.Format("TimeSheet/{0}/{1}/{2}/UpdateResourceBreakTime",UpdateModel.Id,UpdateModel.BreakHours,UpdateModel.BreakTime)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
        //            HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
        //        }
        //        else
        //        {
        //            HttpContext.Current.Session["ErrorMessage"] = ReturnResult.Message;
        //        }
        //    }
        //    return ReturnResult;
        //}

        public static async Task<ReturnModel> EditPMModel(UpdateProjectManagerModel UpdateModel)
        {
            ReturnModel ReturnResult = new ReturnModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("ProjectMangersTimeSheet/UpdatePMTimeSheet"), UpdateModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                    HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = ReturnResult.Message;
                }
            }
            return ReturnResult;
        }

        public static async Task<ReturnModel> RegisterPMBooking(TimeSheetRegisterPMModel RegisterModel)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(TimeSheetAPIURl);
                    HttpResponseMessage response = client.PostAsJsonAsync(string.Format("ProjectMangersTimeSheet/RegisterPMBooking"), RegisterModel).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                        if (ReturnResult.Message == null || ReturnResult.Message == string.Empty)
                        {
                            ReturnResult.Message = "PM Timesheet has been registered successfully.";
                            HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                        }
                        else if (ReturnResult.Message.Contains("Is Already Booked"))
                        {
                            // ReturnResult.Message = "Candidate Timesheet has been registered successfully.";
                            HttpContext.Current.Session["InfoMessage"] = ReturnResult.Message;
                        }
                        else
                        {
                            HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                        }
                    }
                    else
                    {
                        ReturnResult.Message = "There is an issue with the booking. The detail are " + response.ReasonPhrase;
                        HttpContext.Current.Session["ErrorMessage"] = ReturnResult.Message;
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Session["ErrorMessage"] = "There is an issue with the booking. The detail are " + ex.Message;
                }
            }
            return ReturnResult;
        }

        public static async Task<ReturnModel> RegisterHRBooking(TimeSheetRegisterPMModel RegisterModel)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(TimeSheetAPIURl);
                    HttpResponseMessage response = client.PostAsJsonAsync(string.Format("ProjectMangersTimeSheet/RegisterPMBooking"), RegisterModel).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                        if (ReturnResult.Message == null || ReturnResult.Message == string.Empty)
                        {
                            ReturnResult.Message = "PM Timesheet has been registered successfully.";
                            HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                        }
                        else if (ReturnResult.Message.Contains("Is Already Booked"))
                        {
                            // ReturnResult.Message = "Candidate Timesheet has been registered successfully.";
                            HttpContext.Current.Session["InfoMessage"] = ReturnResult.Message;
                        }
                        else
                        {
                            HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                        }
                    }
                    else
                    {
                        ReturnResult.Message = "There is an issue with the booking. The detail are " + response.ReasonPhrase;
                        HttpContext.Current.Session["ErrorMessage"] = ReturnResult.Message;
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Session["ErrorMessage"] = "There is an issue with the booking. The detail are " + ex.Message;
                }
            }
            return ReturnResult;
        }

    }
}