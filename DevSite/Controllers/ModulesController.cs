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

namespace DevSite.Controllers
{
    public class ModulesController : Controller
    {
        private readonly IUnitOfWork uow;

        public ModulesController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        // GET: Modules
        public async Task<IActionResult> Index(int? selected)
        {
            Module selectedModule = null;

            var moduleList = await uow.ModuleRepository.GetAll(includeAll: true);

            if (selected is not null)
            {
                selectedModule = await uow.ModuleRepository.GetOne(Id: selected, includeAll: false);
            }
            else
            {
                selectedModule = null;
            }

            ModuleViewModel moduleIndexModel = new ModuleViewModel
            {
                Modules = moduleList,
                SelectedModule = selectedModule
            };


            return View(moduleIndexModel);
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await uow.ModuleRepository.GetOne(id, includeAll: true);

            if (module == null)
            {
                return NotFound();
            }

            return View(module);
        }

        // GET: Modules/Create
        public async Task<IActionResult> CreateAsync()
        {
            var CourseSelectList = await uow.CourseRepository.GetSelectListItems();
            ViewData["CourseSelectList"] = CourseSelectList;
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartDate,EndDate,CourseId")] Module module)
        {
            if (ModelState.IsValid)
            {
                await uow.ModuleRepository.AddAsync(module);
                await uow.ModuleRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", @module.CourseId);
            return View(module);
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var module = await uow.ModuleRepository.GetOne(id, includeAll: true);
            if (module == null)
            {
                return NotFound();
            }
            var CourseSelectList = await uow.CourseRepository.GetSelectListItems();
            ViewData["CourseSelectList"] = CourseSelectList;
            return View(module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate,CourseId")] Module module)
        {
            if (id != module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    uow.ModuleRepository.Update(module);
                    await uow.ModuleRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(module.Id))
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
            //ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", @module.CourseId);
            return View(module);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await uow.ModuleRepository.GetOne(id, includeAll: false);
                
            if (module == null)
            {
                return NotFound();
            }

            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var module = await uow.ModuleRepository.GetOne(id, includeAll: false);
            uow.ModuleRepository.Remove(module);
            await uow.ModuleRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(int id)
        {
            return uow.ModuleRepository.Any(id);
        }
    }
}
