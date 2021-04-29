using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{

    public class ActivityViewModel
    {
        public List<Activity> Activities { get; set; }

        [Display(Name = "Selected Activity")]
        public Activity SelectedActivity { get; set; }
    }

}
