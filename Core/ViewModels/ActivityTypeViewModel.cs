using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ActivityTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation props 
        public ICollection<Activity> Activities { get; set; }
    }
}
