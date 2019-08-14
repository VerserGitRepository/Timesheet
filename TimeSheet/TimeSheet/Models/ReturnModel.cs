using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ReturnModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}