using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Dto;
using API.Core.Entities;
using AutoMapper;

namespace API.Data.Data
{
    public class MapperProfileAPI : Profile
    {
        public MapperProfileAPI()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Literature, LiteratureDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
        }
    }
}
