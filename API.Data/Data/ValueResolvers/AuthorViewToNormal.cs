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
    public class AuthorViewToNormal : ITypeConverter<AuthorViewModel, Author>
    {
        private readonly IUnitOfWork uow;

        public AuthorViewToNormal(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }
        public Author Convert(AuthorViewModel source, Author destination, ResolutionContext context)
        {
            return uow.AuthorRepository.getAuthor(source.Id, true).Result;
        }
    }
}
