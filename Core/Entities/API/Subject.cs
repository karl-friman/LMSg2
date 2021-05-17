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
    public class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<LiteratureViewModel> Literatures { get; set; }
    }
}
