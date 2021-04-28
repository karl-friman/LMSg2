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
    public class SubjectToView : ITypeConverter<Subject, SubjectViewModel>
    {
        public SubjectViewModel Convert(Subject source, SubjectViewModel destination, ResolutionContext context)
        {
            return new SubjectViewModel()
            {
                Id = source.Id,
                Name = source.Name
            };
        }
    }
}
