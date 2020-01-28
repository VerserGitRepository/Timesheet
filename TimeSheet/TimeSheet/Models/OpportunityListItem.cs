using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TimeSheet.Models
{
    public class OpportunityListItem
    {
        public int Id { get; set; }
        public int Opportunitynumber { get; set; }
        public string Projectname { get; set; }
        public string ProjectManager { get; set; }
        public string SalesManager { get; set; }
        public bool Isactive { get; set; }
        public int ProjectID { get; set; }
        public int ProjectManagerID { get; set; }
        public int SalesManagerID { get; set; }
    }
}