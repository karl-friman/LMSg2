using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{

    public class ActivityListViewModel
    {
        public IEnumerable<ActivityViewModel> Activities { get; set; }

        [Display(Name = "Selected Activity")]
        public ActivityViewModel SelectedActivity { get; set; }
    }

}
