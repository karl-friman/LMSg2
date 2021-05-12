using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class UserDocumentViewModel
    {

        public string DocumentName { get; set; }
        public string CourseName { get; set; }
        public ICollection<Document> CourseDocuments { get; set; }

        public ICollection<ActivityDocumentViewModel> ActivityViewModels { get; set; }
   
        public ICollection<ModuleDocumentViewModel> ModuleViewModels { get; set; }

        public ICollection<Document> LMSUserDocuments{ get; set; }

    }
}

