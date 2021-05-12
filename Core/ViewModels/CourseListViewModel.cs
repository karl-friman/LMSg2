using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class CourseListViewModel
    {
        public IEnumerable<CourseViewModel> Courses { get; set; }

        [Display(Name = "Selected Course")]
        public CourseViewModel SelectedCourse { get; set; }
        public ModuleViewModel SelectedModule { get; set; }
    }
}
