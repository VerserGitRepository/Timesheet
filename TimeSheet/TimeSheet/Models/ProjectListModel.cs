using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheet.Models
{
	public class ProjectListModel
	{

        public int Id { get; set; }
        public string projectName { get; set; }
        public bool IsActive { get; set; }

    }
}