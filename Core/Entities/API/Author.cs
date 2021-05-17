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
    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [JsonIgnore]
        public string FullName => $"{FirstName} {LastName}";

        public List<LiteratureViewModel> Literatures { get; set; }

    }
}
