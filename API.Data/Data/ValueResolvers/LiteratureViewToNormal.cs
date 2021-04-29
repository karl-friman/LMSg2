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
    public class LiteratureViewToNormal : ITypeConverter<LiteratureViewModel, Literature>
    {
        private readonly IUnitOfWork uow;

        public LiteratureViewToNormal(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }
        public Literature Convert(LiteratureViewModel source, Literature destination, ResolutionContext context)
        {
            return uow.LiteratureRepository.getLiterature(source.Id, true).Result;
        }
    }
}
