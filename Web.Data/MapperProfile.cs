using AutoMapper;
using Core.Entities;
using Core.ViewModels;
using System.Collections.Generic;

namespace Lms.Api
{
    public class MapperProfile : Profile
    { 
        public MapperProfile()
        {


            CreateMap<LMSUser, LMSUserViewModel>().ReverseMap();
            CreateMap<Course, CourseViewModel>().ReverseMap();
            CreateMap<Activity, ActivityViewModel>().ReverseMap();           
            CreateMap<Module, ModuleViewModel>().ReverseMap();
        }

    }

}