﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ResourceEventsModel
    {
        public string resourceId { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string projectmanager { get; set; }
        public string Vechicles { get; set; }

    }
}