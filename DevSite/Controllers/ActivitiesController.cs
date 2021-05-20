using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Web.Data.Data;
using Core.ViewModels;
using Core.Repositories;
using Core.Extension;
using AutoMapper;

namespace DevSite.Controllers
{
    public class ActivitiesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ActivitiesController(IUnitOfWork uow,IMapper mapper)
        {
            //_context = context;
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: Activities
        public async Task<IActionResult> Index(int? selected)
        {
            Activity selectedActivity = null;

            List<Activity> activityList = await uow.ActivityRepository.GetAllWithCourseAndModule(includeAll: true);

            if (selected is not null)
            {
                selectedActivity = await uow.ActivityRepository.GetOne(Id: selected, includeAll: false);
            }
            else
            {
                selectedActivity = null;
            }

            var model = mapper.Map<IEnumerable<ActivityViewModel>>(activityList);
            var selectedMapped = mapper.Map<ActivityViewModel>(selectedActivity);

            ActivityListViewModel activityIndexModel = new ActivityListViewModel
            {
                Activities = model,
                SelectedActivity = selectedMapped
            };

            return View(activityIndexModel);
        }

        // GET: Activities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await uow.ActivityRepository.GetOne(id, includeAll: true);
            if (activity == null)
            {
                return NotFound();
            }

            var model = mapper.Map<ActivityViewModel>(activity);
            //var selectedMapped = mapper.Map<CourseViewModel>(selectedCourse);

            return View(model);
        }

        // GET: Activities/Create
        public async Task<IActionResult> Create()
        {
            var ModuleSelectList = await uow.ModuleRepository.GetSelectListItems();
            ViewData["ModuleSelectList"] = ModuleSelectList;
            var ActivityTypeSelectList = await uow.ActivityTypeRepository.GetSelectListItems();
            ViewData["ActivityTypeSelectList"] = ActivityTypeSelectList;
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,EndDate,ModuleId,ActivityTypeId")] Activity activity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await uow.ActivityRepository.AddAsync(activity);
        //        await uow.ActivityRepository.SaveAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    //ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Id", activity.ActivityTypeId);
        //    //ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", activity.ModuleId);
        //    return View(activity);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActivityViewModel activityViewModel)
        {
            if (ActivityExists(activityViewModel.Id))
            {
                ModelState.AddModelError("activityViewModel.Id", "Activity already exists");
            }

            if (ModelState.IsValid)
            {
                var activity = mapper.Map<Activity>(activityViewModel);
                await uow.ActivityRepository.AddAsync(activity);
                await uow.ActivityRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(activityViewModel);
        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await uow.ActivityRepository.GetOne(id, false);
            if (activity == null)
            {
                return NotFound();
            }
            var ModuleSelectList = await uow.ModuleRepository.GetSelectListItems();
            ViewData["ModuleSelectList"] = ModuleSelectList;
            var ActivityTypeSelectList = await uow.ActivityTypeRepository.GetSelectListItems();
            ViewData["ActivityTypeSelectList"] = ActivityTypeSelectList;
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate,ModuleId,ActivityTypeId")] Activity activity)
        {
            if (id != activity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    uow.ActivityRepository.Update(activity);
                    await uow.ActivityRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(activity.Id))
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
            var model = mapper.Map<CourseViewModel>(activity);
            return View(model);
        }

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await uow.ActivityRepository.GetOne(id, includeAll: false);
            if (activity == null)
            {
                return NotFound();
            }

            var model = mapper.Map<ActivityViewModel>(activity);
            return View(model);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activity = await uow.ActivityRepository.GetOne(id, includeAll: false);
            uow.ActivityRepository.Remove(activity);
            await uow.ActivityRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityExists(int id)
        {
            return uow.ActivityRepository.Any(id);
        }
    }
}
