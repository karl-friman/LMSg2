using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class DocumentToAssigmentViewModel
    {

        public int Id { get; set; }

        [Display(Name = "File Name")]
        public string Name { get; set; }
        [Display(Name = "File Description")]
        public string Description { get; set; }
        public IFormFile AssignmentDoc { get; set; }
        // public FileUpload FileUpload { get; set; }
        public string LMSUserId { get; set; }
        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }

        //Navigation props 
        public LMSUser LMSUser { get; set; }


    }
}
