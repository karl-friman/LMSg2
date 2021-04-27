using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LMSUser : IdentityUser
    {
        //public int Id { get; set; }

        public UserType UserType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        //public string Email { get; set; }

        public string Avatar { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        //Foreign Keys
        public int? CourseId { get; set; }

        //Navigation props
        public Course Course { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
