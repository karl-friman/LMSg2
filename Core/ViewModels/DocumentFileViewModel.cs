using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class DocumentFileViewModel
    {
    
        public Course Course { get; set; }
        public Module Module { get; set; }
        //public Activity Activity { get; set; }
        public string FilePath { get; set; }

        public ICollection<Document> Documents { get; set; }
        public ICollection<Module> Modules { get; set; }

       

        public ICollection<Activity> Activities { get; set; }

        //public Document Document { get; set; }


    }
}

// Skriv om UserDocumentViewModel så den bara har en filepath, en actrivity, en module, en kurs