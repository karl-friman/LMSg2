using API.Core.Dto;
using API.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.ViewModel;

namespace API.Core.Util
{
    public class CustomMapper 
    {
        public static AuthorDto MapAuthor(Author author, bool include)
        {
            return new AuthorDto()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth,
                Literatures = include ? author.Literatures.Select(l => new LiteratureViewModel()
                {
                    Id = l.Id,
                    Title = l.Title,
                    PublishDate = l.PublishDate,
                    Description = l.Description,
                    LevelName = l.Level.Name
                }).ToList() : null
            };
        }

        public static LiteratureDto MapLiterature(Literature literature, bool include)
        {
            return new LiteratureDto()
            {
                Id = literature.Id,
                Title = literature.Title,
                PublishDate = literature.PublishDate,
                Description = literature.Description,
                Authors = include ? literature.Authors.Select(a => new AuthorViewModel()
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    DateOfBirth = a.DateOfBirth
                }).ToList() : null,
                Subjects = include ? literature.Subjects.Select(s => new SubjectViewModel()
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList() : null,
                LevelName = literature.Level.Name
                
            };
        }

        public static SubjectDto MapSubject(Subject subject, bool include)
        {
            return new SubjectDto()
            {
                Id = subject.Id,
                Name = subject.Name,
                Literatures = include ? subject.Literatures.Select(l => new LiteratureViewModel()
                {
                    Id = l.Id,
                    Title = l.Title,
                    PublishDate = l.PublishDate,
                    Description = l.Description,
                    LevelName = l.Level.Name
                }).ToList() : null
            };
        }
    }
}
