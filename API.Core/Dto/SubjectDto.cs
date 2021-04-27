﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.ViewModel;

namespace API.Core.Dto
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<LiteratureViewModel> Literatures { get; set; }
    }
}
