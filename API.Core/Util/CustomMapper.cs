using API.Core.Dto;
using API.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Literatures = include ? author.Literatures.Where(l => !string.IsNullOrEmpty(l.Title)).Select(l => l.Title).ToList() : null
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
                Authors = include ? literature.Authors.Select(a => $"{a.FirstName} {a.LastName}").ToList() : null,
                Subjects = include ? literature.Subjects.Select(s => s.Name).ToList() : null,
                LevelName = literature.Level.Name
                
            };
        }

        public static SubjectDto MapSubject(Subject subject, bool include)
        {
            return new SubjectDto()
            {
                Id = subject.Id,
                Name = subject.Name,
                Literatures = include ? subject.Literatures.Select(l => l.Title).ToList() : null
            };
        }
    }
}
