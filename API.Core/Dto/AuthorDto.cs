﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.ViewModel;
using Newtonsoft.Json;

namespace API.Core.Dto
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        [JsonIgnore]
        public string FullName => $"{FirstName} {LastName}";
        public ICollection<LiteratureViewModel> Literatures { get; set; }

    }
}
