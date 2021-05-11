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
    public class DashboardModulesViewComponent : ViewComponent
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public DashboardModulesViewComponent(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            var user = await uow.LMSUserRepository.GetOne(userId, true);
            var modules = user.Course.Modules.OrderBy(d => d.StartDate).ToList();
            //var activities = modules.SelectMany(m => m.modu).OrderBy(d => d.StartDate).ToList();

            var model = new DashboardStudentModulesViewModel
            {
                Modules = modules
            };

            return View(model);
        }
    }
}
