using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ModuleDocumentViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

       public string FilePath { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
