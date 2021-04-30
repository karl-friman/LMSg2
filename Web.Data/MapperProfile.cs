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


            CreateMap<Course, CourseViewModel>()
                          .ForMember(
                           courses => courses.Courses,
                           opt => opt.MapFrom(src =>new List<Course> { 
                           
                               new Course
                               {

                                    Name = src.Name,
                                    Description = src.Description,
                                    StartDate = src.StartDate,
                                    EndDate = src.EndDate
                               }
                           
                           }))
                          .ForMember(
                           course => course.SelectedCourse,
                           opt => opt.MapFrom(src => 
                               new Course
                               {
                                    Name = src.Name,
                                    Description = src.Description,
                                    StartDate = src.StartDate,
                                    EndDate = src.EndDate
                               }

                           )).ReverseMap();



            CreateMap<Activity, ActivityViewModel>()
                          .ForMember(
                           activities => activities.Activities,
                           opt => opt.MapFrom(src => new List<Activity> {

                               new Activity
                               {

                                    Name = src.Name,
                                    Description = src.Description,
                                    StartDate = src.StartDate,
                                    EndDate = src.EndDate
                               }

                           }))
                          .ForMember(
                           activity => activity.SelectedActivity,
                           opt => opt.MapFrom(src =>
                               new Activity
                               {
                                   Name = src.Name,
                                   Description = src.Description,
                                   StartDate = src.StartDate,
                                   EndDate = src.EndDate
                               }

                           )).ReverseMap();



            CreateMap<Module, ModuleViewModel>()
                          .ForMember(
                           modules => modules.Modules,
                           opt => opt.MapFrom(src => new List<Module> {

                               new Module
                               {

                                    Name = src.Name,
                                    Description = src.Description,
                                    StartDate = src.StartDate,
                                    EndDate = src.EndDate
                               }

                           }))
                          .ForMember(
                           module => module.SelectedModule,
                           opt => opt.MapFrom(src =>
                               new Activity
                               {
                                   Name = src.Name,
                                   Description = src.Description,
                                   StartDate = src.StartDate,
                                   EndDate = src.EndDate
                               }

                           )).ReverseMap();




















        }

    }

}