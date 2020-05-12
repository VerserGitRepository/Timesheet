using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.ServiceHelper
{
    public class ValidateUpdates
    {      
            public static bool ValidateConfirmBooking(int bookingStatusId)
            {
            bool returnFlag = false;
            switch (bookingStatusId)
            {
                case 10:
                    if (UserRoles.UserCanConfirmBookings() == true)
                    {
                        returnFlag= true;
                    }
                    break;

                case 2:
                    if (UserRoles.UserCanConfirmBookings() == true)
                    {
                        returnFlag= true;
                    }
                    break;
                case 3:
                    if (UserRoles.UserCanConfirmBookings() == true)
                    {
                        returnFlag = true;
                    }
                    break;
                case 5:
                    if (UserRoles.UserCanConfirmBookings() == true)
                    {
                        returnFlag = true;
                    }
                    break;
                default:
                    returnFlag = false;
                    break;
               }
            return returnFlag;
          }        
    }
}