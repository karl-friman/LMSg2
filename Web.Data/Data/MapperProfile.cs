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
            CreateMap<DocumentToAssigmentViewModel, Document>()
                .ForMember(dest => dest.FilePath,
                  from => from.MapFrom(m => m.AssignmentDoc.FileName)).ReverseMap();
            CreateMap<ActivityType, ActivityTypeViewModel>().ReverseMap();
           
        }

    }
}