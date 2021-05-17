using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.API.FormModels
{
    public class LiteratureCreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string AuthorsIds { get; set; }
        public string SubjectsIds { get; set; }
        public string LevelName { get; set; }
    }
}
