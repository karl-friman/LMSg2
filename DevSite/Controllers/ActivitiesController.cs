﻿using System;
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
using AutoMapper;
using Core.ViewModels.ActivitiesViewModel;

namespace DevSite.Controllers
{
    public class ActivitiesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ActivitiesController(IUnitOfWork uow, IMapper mapper)
        {
            //_context = context;
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: Activities
        public async Task<IActionResult> Index(int? selected)
        {
            //Activity selectedActivity = null;

            //List<Activity> activityList = await uow.ActivityRepository.GetAll(includeAll: true);

            //if (selected is not null)
            //{
            //    selectedActivity = await uow.ActivityRepository.GetOne(Id: selected, includeAll: false);
            //}
            //else
            //{
            //    selectedActivity = null;
            //}

            //ActivityViewModel activityIndexModel = new ActivityViewModel
            //{
            //    Activities = activityList,
            //    SelectedActivity = selectedActivity
            //};

            //return View(activityIndexModel);
            var modules = await uow.ActivityRepository.GetAll(includeAll: true); ;
            var model = mapper.Map<IEnumerable<ActivityViewModel>>(modules);
            return View(model);



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

            return View(activity);
        }

        // GET: Activities/Create
        public IActionResult Create()
        {
            //ViewData["ActivityTypeId"] = new SelectList(uow.ActivityTypes.GetAll(false), "Id", "Id");
            //ViewData["ModuleId"] = new SelectList(uow.ModuleRepository.GetAll(false), "Id", "Id");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,EndDate,ModuleId,ActivityTypeId")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                await uow.ActivityRepository.AddAsync(activity);
                await uow.ActivityRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Id", activity.ActivityTypeId);
            //ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", activity.ModuleId);
            return View(activity);
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
            //ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Id", activity.ActivityTypeId);
            //ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", activity.ModuleId);
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
            //ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Id", activity.ActivityTypeId);
            //ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", activity.ModuleId);
            return View(activity);
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

            return View(activity);
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
