using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class LMSUserViewModel : IdentityUser
    {
        public UserType UserType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Avatar { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        //Foreign Keys
        [Display(Name = "Course")]
        public int? CourseId { get; set; }

        //Navigation props
        public CourseViewModel Course { get; set; }
        public ICollection<DocumentViewModel> Documents { get; set; }
    }
}
