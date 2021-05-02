﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.ModulesViewModel
{
    class ModuleListViewModel
    {

        public string Name { get; set; }
        [Display(Name = "Course Description")]
        public string Description { get; set; }
        [Display(Name = "Course Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Course End Date")]
        public DateTime EndDate { get; set; }

       
    }
}
