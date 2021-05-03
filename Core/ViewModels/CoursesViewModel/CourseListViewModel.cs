using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.CoursesViewModel
{
    class CourseListViewModel
    {
        public List<CourseViewModel> Courses { get; set; }
        [Display(Name = "Selected Course")]
        public Course SelectedCourse { get; set; }

    }
}
