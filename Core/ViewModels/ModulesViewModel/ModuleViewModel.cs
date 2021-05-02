using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.ModulesViewModel
{
    public class ModuleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Course Description")]
        public string Description { get; set; }
        [Display(Name = "Course Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Course End Date")]
        public DateTime EndDate { get; set; }

        public Course Course { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Document> Documents { get; set; }

        

        //public List<Module> Modules { get; set; }

        //[Display(Name = "Selected Module")]
        //public Module SelectedModule { get; set; }

    }
}
