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
                        else if (ReturnResult.Message.Contains("Is Already Booked"))
                        {
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
            var ReturnResult = new ReturnModel();

            if (UpdateModel.StatusID == 7 || UpdateModel.StatusID == 4)
            {
                if (string.IsNullOrEmpty(UpdateModel.ActualQuantity.ToString()))
                {
                    ReturnResult.IsSuccess = false;
                    HttpContext.Current.Session["ErrorMessage"] = "Actual Quantity Required To Complete or Approve Booking!";
                    return ReturnResult;
                }
            }
            if (UpdateModel.StatusID == 2 || UpdateModel.StatusID == 3)
            {
                if (ValidateUpdates.ValidateConfirmBooking(Convert.ToInt32(UpdateModel.StatusID)) != true)
                {
                    ReturnResult.IsSuccess = false;
                    HttpContext.Current.Session["ErrorMessage"] = "Your UserName Doesn't Obtained Status Change Permission !";
                    return ReturnResult;
                }
            }
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
        public static async Task<ReturnModel> Confirmbooking(string bookingId, string FullName)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format($"timesheet/confirmbooking/{bookingId}/{FullName}")).Result;
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
        public static async Task<ReturnModel> EditHRModel(UpdateHRModel UpdateModel)
        {
            ReturnModel ReturnResult = new ReturnModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format("RecruiterTimesheet/UpdateHRTimeSheet"), UpdateModel).Result;
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
        public static async Task<ReturnModel> RegisterHRBooking(HRRegisterViewModel RegisterModel)
        {
            ReturnModel ReturnResult = new ReturnModel();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(TimeSheetAPIURl);
                    HttpResponseMessage response = client.PostAsJsonAsync(string.Format("RecruiterTimesheet/RegisterHRTimeSheet"), RegisterModel).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ReturnResult = await response.Content.ReadAsAsync<ReturnModel>();
                        if (ReturnResult.Message == null || ReturnResult.Message == string.Empty)
                        {
                            ReturnResult.Message = "HR Timesheet has been registered successfully.";
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
        public static async Task<string> FetchActuals(string bookingId)
        {
            string ReturnResult = "";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.GetAsync(string.Format($"timesheet/{bookingId}/ActivityActuals")).Result;
                if (response.IsSuccessStatusCode)
                {
                    ReturnResult = await response.Content.ReadAsStringAsync();
                    //HttpContext.Current.Session["ResultMessage"] = ReturnResult.Message;
                }
                else
                {
                    HttpContext.Current.Session["ErrorMessage"] = "Error occurred in the request.";
                }
            }
            return ReturnResult;
        }
        public static async Task<ReturnModel> Rejectbooking(string bookingId, string comments)
        {
            ReturnModel ReturnResult = new ReturnModel();
            ListItems item = new ListItems();
            item.Id = int.Parse(bookingId);
            item.Value = comments;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(TimeSheetAPIURl);
                HttpResponseMessage response = client.PostAsJsonAsync(string.Format($"TimeSheet/RemoveRejectedBookings"), item).Result;
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
    }
}