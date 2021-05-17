using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Web.Data.Data;
using Core.Repositories;
using AutoMapper;
using Core.ViewModels;

namespace DevSite.Controllers
{
    public class ActivityTypesController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;


        public ActivityTypesController(IUnitOfWork uow,IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: ActivityTypes
        public async Task<IActionResult> Index()
        {
            // return View(await uow.ActivityTypeRepository.GetAll(includeAll: false));
            var activitiesTypeList = await uow.ActivityTypeRepository.GetAllWithCourseAndModule(includeAll: false);
            var model = mapper.Map<IEnumerable<ActivityTypeViewModel>>(activitiesTypeList);
            return View(model);
        }

        // GET: ActivityTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityType = await uow.ActivityTypeRepository.GetOne(id, false);
            if (activityType == null)
            {
                return NotFound();
            }

            var model = mapper.Map<ActivityTypeViewModel>(activityType);
            return View(model);
        }

        // GET: ActivityTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActivityTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActivityTypeViewModel activityTypeViewModel)
        {
            if (ActivityTypeExists(activityTypeViewModel.Id))
            {
                ModelState.AddModelError("DocumentId", "Document already exists");
            }

            if (ModelState.IsValid)
            {
                var activittyType = mapper.Map<ActivityType>(activityTypeViewModel);
                await uow.ActivityTypeRepository.AddAsync(activittyType);
                await uow.ActivityTypeRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(activityTypeViewModel);
        }

        // GET: ActivityTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityType = await uow.ActivityTypeRepository.GetOne(id,false);
            if (activityType == null)
            {
                return NotFound();
            }
            return View(activityType);
        }

        // POST: ActivityTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ActivityType activityType)
        {
            if (id != activityType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    uow.ActivityTypeRepository.Update(activityType);
                    await uow.ActivityTypeRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityTypeExists(activityType.Id))
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
            var model = mapper.Map<ActivityTypeViewModel>(activityType);
            return View(model);
        }

        // GET: ActivityTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityType = await uow.ActivityTypeRepository.GetOne(id,false);
            if (activityType == null)
            {
                return NotFound();
            }

            var model = mapper.Map<ActivityTypeViewModel>(activityType);
            return View(model);
        }

        // POST: ActivityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activityType = await uow.ActivityTypeRepository.GetOne(id, false);
            uow.ActivityTypeRepository.Remove(activityType);

            await uow.ActivityTypeRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityTypeExists(int id)
        {
            return uow.ActivityTypeRepository.Any(id);
        }
    }
}
