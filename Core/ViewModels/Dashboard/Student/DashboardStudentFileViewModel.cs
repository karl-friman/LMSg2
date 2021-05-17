using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Dashboard.Student
{
    class DashboardStudentFileViewModel
    {

        //documentFolderName: fullname - firstname + "_" lastname . If file exists do windows style = adds: (1) if same filename (then 2)


        public string Name { get; set; }
        public Course Course { get; set; }
        public ICollection<Document> Documents { get; set; }
 
    }
}
