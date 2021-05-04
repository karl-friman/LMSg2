using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class LMSUserListViewModel
    {
        public List<LMSUser> LMSUsers { get; set; }
        //public UserType UserType { get; set; }
        //public string FullName { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public string Avatar { get; set; }
        //public string CourseName { get; set; }
        //public string Email { get; set; }
        //public string PhoneNumber { get; set; }

        [Display(Name = "Selected User")]
        public LMSUser SelectedUser { get; set; }
    }
}
