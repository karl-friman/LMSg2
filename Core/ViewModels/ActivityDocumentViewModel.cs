﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ActivityDocumentViewModel
    {
        public string Name { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
