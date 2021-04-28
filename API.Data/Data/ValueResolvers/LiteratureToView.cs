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
    public class LiteratureToView : ITypeConverter<Literature, LiteratureViewModel>
    {

        public LiteratureViewModel Convert(Literature source, LiteratureViewModel destination, ResolutionContext context)
        {
            return new LiteratureViewModel()
            {
                Id = source.Id,
                Title = source.Title,
                PublishDate = source.PublishDate,
                Description = source.Description,
                LevelName = source.Level.Name
            };
        }

    }
}
