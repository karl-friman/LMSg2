using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.ViewModel
{
    public class SubjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<string> Literatures { get; set; }
    }
}
