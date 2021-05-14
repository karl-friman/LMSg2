using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class AdminDocumentViewModel
    {  
        public List<string> CourseNames { get; set; }
        public List<ICollection<Document>> CourseDocuments { get; set; }

        public List<ICollection<Document>> ActivityViewModels { get; set; }
   
        public List<ICollection<Document>> ModuleViewModels { get; set; }

    }
}

