using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.API;
using Core.Entities.API.ViewModels;
using Newtonsoft.Json;

namespace Core.Entities.API
{
    public class Literature
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishDate { get; set; }

        public string Description { get; set; }

        public List<AuthorViewModel> Authors { get; set; }

        public List<SubjectViewModel> Subjects { get; set; }

        public string LevelName { get; set; }

    }
}
