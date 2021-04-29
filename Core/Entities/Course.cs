using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        [Display(Name = "Course")]
        public string Name { get; set; }
        [Display(Name = "Course Description")]
        public string Description { get; set; }
        [Display(Name = "Course Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Course End Date")]
        public DateTime EndDate { get; set; }

        //Navigation props 
        [Display(Name = "Students")]
        public ICollection<LMSUser> Users { get; set; }
        [Display(Name = "Modules")]
        public ICollection<Module> Modules { get; set; }
        [Display(Name = "Documents")]
        public ICollection<Document> Documents { get; set; }

    }
}
