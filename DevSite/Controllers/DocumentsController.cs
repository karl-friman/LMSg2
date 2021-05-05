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
using AutoMapper;

namespace DevSite.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public DocumentsController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            var doucmentList = await uow.DocumentRepository.GetAll(includeAll: true);
            var model = mapper.Map<IEnumerable<DocumentViewModel>>(doucmentList);
            return View(model);
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await uow.DocumentRepository.GetOne(id, includeAll: true);
            if (document == null)
            {
                return NotFound();
            }

            var model = mapper.Map<DocumentViewModel>(document);
            return View(model);
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Description,TimeStamp,FilePath,UserId,CourseId,ModuleId,ActivityId")] Document document)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await uow.DocumentRepository.AddAsync(document);
        //        await uow.DocumentRepository.SaveAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(document);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentViewModel documentViewModel)
        {
            if (DocumentExists(documentViewModel.Id))
            {
                ModelState.AddModelError("DocumentId", "Document already exists");
            }

            if (ModelState.IsValid)
            {
                var document = mapper.Map<Document>(documentViewModel);
                await uow.DocumentRepository.AddAsync(document);
                await uow.DocumentRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentViewModel);
        }



        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await uow.DocumentRepository.GetOne(id, false);
            if (document == null)
            {
                return NotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,TimeStamp,FilePath,UserId,CourseId,ModuleId,ActivityId")] Document document)
        {
            if (id != document.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    uow.DocumentRepository.Update(document);
                    await uow.DocumentRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.Id))
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
            var model = mapper.Map<DocumentViewModel>(document);
            return View(model);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await uow.DocumentRepository.GetOne(id, includeAll: false);
            if (document == null)
            {
                return NotFound();
            }

            var model = mapper.Map<DocumentViewModel>(document);
            return View(model);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await uow.DocumentRepository.GetOne(id, includeAll: false);
            uow.DocumentRepository.Remove(document);
            await uow.DocumentRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int id)
        {
            return uow.DocumentRepository.Any(id);
        }
    }
}
