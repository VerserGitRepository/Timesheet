using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
    public class ResourceRatingModel
    {
        public int Id { get; set; }
        public int CandidateTimeSheetID { get; set; }         
        public int Punctuality { get; set; }
        public int ProfessionalService { get; set; }
        public int PoliteAndCourteous { get; set; }
        public int WorkQuality { get; set; }
        public string Comments { get; set; }
        public string User { get; set; }
    }
}