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

        public ICollection<ActivityDocumentViewModel> ActivityViewModels { get; set; }

        public ICollection<ModuleDocumentViewModel> ModuleViewModels { get; set; }

       public ICollection<Document> LMSUserDocuments { get; set; }

    }
}

