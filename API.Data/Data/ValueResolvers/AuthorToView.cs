using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Entities;
using API.Core.ViewModel;
using AutoMapper;

namespace API.Data.Data.ValueResolvers
{
    public class AuthorToView : ITypeConverter<Author, AuthorViewModel>
    {
        public AuthorViewModel Convert(Author source, AuthorViewModel destination, ResolutionContext context)
        {
            return new AuthorViewModel()
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                DateOfBirth = source.DateOfBirth
            };
        }
    }
}
