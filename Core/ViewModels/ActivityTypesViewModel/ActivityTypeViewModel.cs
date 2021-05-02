using System.Collections.Generic;
using System.Diagnostics;

namespace Core.ViewModels.CoursesViewModel
{
 public class ActivityTypeViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Activity> Activities { get; set; }



    }
}