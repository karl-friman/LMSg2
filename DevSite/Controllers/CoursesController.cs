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

namespace DevSite.Controllers
{
    public class CoursesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public CoursesController(IUnitOfWork uow, IMapper mapper)
        {
            //_context = context;
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
            var selectMapped = mapper.Map<CourseViewModel>(selectedCourse);

            CourseListViewModel courseIndexModel = new CourseListViewModel
            {
                Courses = model,
                SelectedCourse = selectMapped
            };

            return View(courseIndexModel);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                await uow.CourseRepository.AddAsync(course);
                await uow.CourseRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
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
                return RedirectToAction(nameof(Index));
            }
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

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await uow.CourseRepository.GetOne(id, includeAll: false);
            uow.CourseRepository.Remove(course);
            await uow.CourseRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return uow.CourseRepository.Any(id);
        }
    }
}
