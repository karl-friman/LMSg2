using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Dto;
using API.Core.Entities;
using API.Core.Repositories;
using API.Core.ViewModel;
using API.Data.Data.ValueResolvers;
using AutoMapper;

namespace API.Data.Data
{
    public class MapperProfileAPI : Profile
    {

        public MapperProfileAPI()
        {

            CreateMap<Author, AuthorViewModel>().ConvertUsing<AuthorToView>();
            CreateMap<AuthorViewModel, Author>().ConvertUsing<AuthorViewToNormal>();

            CreateMap<Literature, LiteratureViewModel>().ConvertUsing<LiteratureToView>();
            CreateMap<LiteratureViewModel, Literature>().ConvertUsing<LiteratureViewToNormal>();

            CreateMap<Subject, SubjectViewModel>().ConvertUsing<SubjectToView>();
            CreateMap<SubjectViewModel, Subject>().ConvertUsing<SubjectViewToNormal>();


            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Literature, LiteratureDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();

        }

    }

}
