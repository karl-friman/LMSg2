using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
        public string FilePath { get; set; }
        // public FileUpload FileUpload { get; set; }

        //Foreign key
        public int LMSUserId { get; set; }
        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }

        //Navigation props 
        public LMSUser LMSUser { get; set; }
        
        //has been added
        //public Course Course { get; set; }
        //public Module Module { get; set; }
        //public Activity Activity { get; set; }

        
    }
}
