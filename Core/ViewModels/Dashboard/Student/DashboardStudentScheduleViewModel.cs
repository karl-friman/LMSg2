using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Dashboard.Student
{
    public class DashboardStudentScheduleViewModel
    {

        public string FullName { get; set; }

        //Navigation props
        public Course Course { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
