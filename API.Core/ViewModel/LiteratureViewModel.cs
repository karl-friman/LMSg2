﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.ViewModel
{
    public class LiteratureViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
        public ICollection<string> Authors { get; set; }
        public ICollection<string> Subjects { get; set; }
        public string LevelName { get; set; }
    }
}
