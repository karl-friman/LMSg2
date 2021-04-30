using Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ModuleViewModel
    {
        public List<Module> Modules { get; set; }

        [Display(Name = "Selected Module")]
        public Module SelectedModule { get; set; }
        public IEnumerable<SelectListItem> CourseSelectList { get; set; }
    }
}
