using AutoMapper;
using Core.Repositories;
using Core.ViewModels;
using Core.ViewModels.Dashboard.Student;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Web.ViewComponents.Dashboard.Student
{
    //[ViewComponent]
    public class DashboardScheduleViewComponent : ViewComponent
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public DashboardScheduleViewComponent(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            var user = await uow.LMSUserRepository.GetOne(userId, true);

            var modules = user.Course.Modules;
            var activities = modules.SelectMany(a => a.Activities).OrderBy(d => d.StartDate).ToList();

            var model = new DashboardStudentScheduleViewModel
            {
                Course = user.Course,
                FullName = user.FullName,
                Activities = activities

            };

            return View(model);
        }
    }
}
