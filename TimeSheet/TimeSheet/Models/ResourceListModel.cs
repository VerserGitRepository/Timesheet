﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ResourceListModel
    {
        public string id { get; set; }
        public string ResourceName { get; set; }
        public string WarehouseName { get; set; }
        public bool IsActive { get; set; }
        public string title { get; set; }
        public string eventColor { get; set; }
        public string ActivityDescription { get; set; }
        public string Vechicles { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime{ get; set; }
        public string ProjectManager { get; set; }
        public string Warehouse { get; set; }
        public int BookingId { get; set; }
        public int JobId { get; set; }
    }
}