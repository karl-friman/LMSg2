using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ActivityTypeListViewModel
    {
        public IEnumerable<ActivityTypeViewModel> ActivitiesTypes { get; set; }

        [Display(Name = "Selected ActivityType")]
        public ActivityTypeViewModel SelectedActivityType { get; set; }
    }
}
