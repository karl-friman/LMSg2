﻿using System;
using System.Collections.Generic;
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
        public int ModuleId { get; set; }
        public int ActivityTypeId { get; set; }
        //Navigation props 
        public Module Module { get; set; }
        public ActivityType ActivityType { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
