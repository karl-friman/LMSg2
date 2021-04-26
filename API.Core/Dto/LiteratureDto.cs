using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Dto
{
    public class LiteratureDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }

        public ICollection<AuthorDto> Authors { get; set; }

        public ICollection<SubjectDto> Subjects { get; set; }

        public string LevelName { get; set; }

    }
}
