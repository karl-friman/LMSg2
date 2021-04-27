using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ActivityType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation props 
        //public ICollection<Activity> Activity { get; set; }
    }
}
