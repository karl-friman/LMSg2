using AutoMapper;
using Core.Entities;
using Core.ViewModels;
using Core.ViewModels.ActivitiesViewModel;
using Core.ViewModels.CoursesViewModel;
using Core.ViewModels.ModulesViewModel;
using Core.ViewModels.LMSUsersViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<LMSUser, LMSUserViewModel>().ReverseMap();
            CreateMap<Course, CourseViewModel>().ReverseMap();
            CreateMap<Activity, ActivityViewModel>().ReverseMap();
            CreateMap<Module, ModuleViewModel>().ReverseMap();
            CreateMap<ActivityType, ActivityTypeViewModel>().ReverseMap();
        }

    }
}
