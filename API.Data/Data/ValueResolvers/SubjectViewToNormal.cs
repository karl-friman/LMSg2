using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Entities;
using API.Core.Repositories;
using API.Core.ViewModel;
using AutoMapper;

namespace API.Data.Data.ValueResolvers
{
    public class SubjectViewToNormal : ITypeConverter<SubjectViewModel, Subject>
    {

        private readonly IUnitOfWork uow;

        public SubjectViewToNormal(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }
        public Subject Convert(SubjectViewModel source, Subject destination, ResolutionContext context)
        {
            return uow.SubjectRepository.getSubjects(source.Id, true).Result;
        }
    }
}
