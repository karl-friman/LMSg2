using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class LMSUserViewModel
    {
        public UserType UserType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Avatar { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        //Foreign Keys
        public int? CourseId { get; set; }

        //Navigation props
        public Course Course { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
