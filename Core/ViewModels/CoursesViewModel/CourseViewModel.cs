using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.CoursesViewModel
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Course Description")]
        public string Description { get; set; }
        [Display(Name = "Course Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Course End Date")]
        public DateTime EndDate { get; set; }

        public ICollection<Module> Modules { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Document> Documents { get; set; }

        [Display(Name = "Modules")]
        public int ModuleId { get; set; }
        [Display(Name = "Activities")]
        public int ActivityId { get; set; }
        [Display(Name = "Documents")]
        public int DocumentId { get; set; }





    }
}
