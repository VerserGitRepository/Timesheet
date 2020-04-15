using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeSheet.Models.DTOs;
namespace TimeSheet.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApprovedTimesheetModel, ExportApprovedTimesheetDto>();
        }
    }
}