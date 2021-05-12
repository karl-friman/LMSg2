using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.ViewModels;

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
            CreateMap<Document, DocumentViewModel>().ReverseMap();
            CreateMap<ActivityType, ActivityTypeViewModel>().ReverseMap();
            //CreateMap<Activity, UserDocumentViewModel>().ReverseMap();
            //CreateMap<Document, UserDocumentViewModel>().ReverseMap();
            //CreateMap<Module, UserDocumentViewModel>().ReverseMap();

            //.IncludeMembers(Course => new Course())
            //.IncludeMembers(Module => new Module())
            //.IncludeMembers(Activity => new Activity()).ReverseMap();


        }

    }
}