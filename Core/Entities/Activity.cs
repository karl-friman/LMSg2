using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //Foreign key
        [Display(Name = "Modules")]
        public int ModuleId { get; set; }
        [Display(Name = "Activity Type")]
        public int? ActivityTypeId { get; set; }
        //Navigation props 
        public Module Module { get; set; }
        public ActivityType ActivityType { get; set; }
        [Display(Name = "Documents")]
        public ICollection<Document> Documents { get; set; }
    }
}
