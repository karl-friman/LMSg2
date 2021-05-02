using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.ActivitiesViewModel
{

    public class ActivityViewModel
    {
        //public List<Activity> Activities { get; set; }

        //[Display(Name = "Selected Activity")]
        //public Activity SelectedActivity { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Module Module { get; set; }
        public ActivityType ActivityType { get; set; }
        [Display(Name = "Documents")]
        public ICollection<Document> Documents { get; set; }







    }

}
