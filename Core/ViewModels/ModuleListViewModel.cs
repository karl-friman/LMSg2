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
    public class ModuleListViewModel
    {
        public IEnumerable<ModuleViewModel> Modules { get; set; }

        [Display(Name = "Selected Course")]
        public ModuleViewModel SelectedModule { get; set; }
        public IEnumerable<SelectListItem> CourseSelectList { get; set; }
    }
}
