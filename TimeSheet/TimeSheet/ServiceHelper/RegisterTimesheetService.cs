﻿using System;
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

        public static async Task<ReturnModel> EditPMModel(UpdateProjectManagerModel UpdateModel)
        {
            ReturnModel ReturnResult = new ReturnModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("ProjectMangers/UpdatePMTimeSheet"), UpdateModel).Result;
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
                    HttpResponseMessage response = client.PostAsJsonAsync(string.Format("ProjectMangersTimesheet/RegisterPMBooking"), RegisterModel).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                        if (ReturnResult.Message == null || ReturnResult.Message == string.Empty)
                        {
                            ReturnResult.Message = "PM Timesheet has been registered successfully.";
                            HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
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