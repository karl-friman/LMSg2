using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core.Entities
{
    public class Literature
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }

        public ICollection<Author> Authors { get; set; }

        public ICollection<Subject> Subjects { get; set; }

        public Level Level { get; set; }
    }
}
