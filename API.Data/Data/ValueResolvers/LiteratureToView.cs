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
                Authors = source.Authors.Select(a => $"{a.Id}, {a.FirstName} {a.LastName}").ToList(),
                Subjects = source.Subjects.Select(s => $"{s.Id}, {s.Name}").ToList(),
                // om level inte matchar något i databasen så blir det automatiskt beginner
                LevelName = source.Level?.Name ?? "Beginner"
            };
        }

    }
}
