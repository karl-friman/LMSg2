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

namespace DevSite.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UserManager<LMSUser> _userManager;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public CoursesController(UserManager<LMSUser> userManager, IUnitOfWork uow, IMapper mapper)
        {
            _userManager = userManager;
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int? selected)
        {
            Course selectedCourse = null;

            var courseList = await uow.CourseRepository.GetAll(includeAll: true);

            if (selected is not null)
            {
                selectedCourse = await uow.CourseRepository.GetOne(Id: selected, includeAll: true);
            }
            else
            {
                selectedCourse = null;
            }

            var model = mapper.Map<IEnumerable<CourseViewModel>>(courseList);
            var selectedMapped = mapper.Map<CourseViewModel>(selectedCourse);

            CourseListViewModel courseIndexModel = new CourseListViewModel
            {
                Courses = model,
                SelectedCourse = selectedMapped
            };

            return View(courseIndexModel);
        }

        public async Task<IActionResult> Assignments()
        {
            string userId = _userManager.GetUserId(User);
            LMSUser user = await uow.LMSUserRepository.GetOne(userId, includeAll: false);
            Course course = await uow.CourseRepository.GetOne(user.CourseId, includeAll: true);
            IEnumerable<Activity> activities = course.Modules
                                                .SelectMany(a => a.Activities)
                                                .ToList()
                                                .Where(a=>a.ActivityType.Name == "Assignment");

            IEnumerable<ActivityViewModel> model = mapper.Map<IEnumerable<ActivityViewModel>>(activities);
            return View(model);
            //Fråga: går det att skriva mer effektiv kod med LINQ?
            //Hur få med documents?
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await uow.CourseRepository.GetOne(id, includeAll: true);

            if (course == null)
            {
                return NotFound();
            }

            var model = mapper.Map<CourseViewModel>(course);
            //var selectedMapped = mapper.Map<CourseViewModel>(selectedCourse);

            return View(model);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,EndDate")] Course course)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await uow.CourseRepository.AddAsync(course);
        //        await uow.CourseRepository.SaveAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(course);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel courseViewModel)
        {

            if (CourseExists(courseViewModel.Id))
            {
                ModelState.AddModelError("CourseId", "Course already exists");
            }

            if (ModelState.IsValid)
            {
                var course = mapper.Map<Course>(courseViewModel);
                await uow.CourseRepository.AddAsync(course);
                await uow.CourseRepository.SaveAsync();
                return RedirectToAction(nameof(AddSuccess));
            }
            return View(courseViewModel);
        }
   
        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await uow.CourseRepository.GetOne(id, includeAll: true);
            if (course == null)
            {
                return NotFound();
            }

          //  var model = mapper.Map<CourseViewModel>(course);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    uow.CourseRepository.Update(course);
                    await uow.CourseRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(EditSuccess));
               
            }
            var model = mapper.Map<CourseViewModel>(course);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await uow.CourseRepository.GetOne(id, includeAll: false);
            if (course == null)
            {
                return NotFound();
            }
            var model = mapper.Map<CourseViewModel>(course);
            return View(model);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await uow.CourseRepository.GetOne(id, includeAll: false);
           // var model = mapper.Map<CourseViewModel>(course);
            uow.CourseRepository.Remove(course);
            await uow.CourseRepository.SaveAsync();
            return RedirectToAction(nameof(DeleteSuccess));
        }

        private bool CourseExists(int id)
        {
            return uow.CourseRepository.Any(id);
        }

        public IActionResult AddSuccess()
        {
            return View();
        }

        public IActionResult DeleteSuccess()
        {
            return View();
        }

        public IActionResult EditSuccess()
        {
            return View();
        }


    }
}
