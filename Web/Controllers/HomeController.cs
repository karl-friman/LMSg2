using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.ViewModels;
using Web.Data.Data;
using Core.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger, UserManager<LMSUser> userManager, IUnitOfWork uow, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courseList = await uow.CourseRepository.GetAll(includeAll: true);
            var model = mapper.Map<IEnumerable<CourseViewModel>>(courseList);

            CourseListViewModel courseIndexModel = new CourseListViewModel
            {
                Courses = model,
            };

            return View(courseIndexModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
