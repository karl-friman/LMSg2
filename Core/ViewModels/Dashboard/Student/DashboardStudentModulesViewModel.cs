using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Dashboard.Student
{
    public class DashboardStudentModulesViewModel
    {

        //public string FullName { get; set; }

        //Navigation props
        //public Course Course { get; set; }
        public ICollection<Module> Modules { get; set; }
    }
}
