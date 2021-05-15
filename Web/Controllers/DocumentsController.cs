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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.IO;



namespace Web.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private IWebHostEnvironment environment;
        private readonly UserManager<LMSUser> _userManager;

        public DocumentsController(IUnitOfWork uow, IMapper mapper, UserManager<LMSUser> _userManager, IWebHostEnvironment environment)
        {
            this.uow = uow;
            this.mapper = mapper;
            this._userManager = _userManager;
            this.environment = environment;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            var doucumentList = await uow.DocumentRepository.GetAll(includeAll: true);
            var model = mapper.Map<IEnumerable<DocumentViewModel>>(doucumentList);
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

        //GET
        public async Task<IActionResult> StudentFilesView()
        {
            var userId = _userManager.GetUserId(User);
            var currentUser = await uow.LMSUserRepository.GetOne(userId, false);
            var allDocuments = await uow.DocumentRepository.GetAll(false);
            var userDocuments = allDocuments.Where(u => u.LMSUser == currentUser);

            var model = mapper.Map<IEnumerable<DocumentViewModel>>(userDocuments);

            return View(model);
        }

        //GET
        public async Task<IActionResult> StudentFiles()
        {

            //efd031d8-4f8a-4fed-bc12-39794139adfc
            var userId = _userManager.GetUserId(User);
            //var testId = "341f2735-a809-4923-9588-094e5c6c2a33";
            //var currentUser = await uow.LMSUserRepository.GetOne(userId, false);
            var currentUser = await uow.LMSUserRepository.GetOne(userId, true);
            //Course han går på ----ingen activity is linked to the course activity

            Course course = currentUser.Course;
            int courseId = (int)currentUser.CourseId;
            string name = course.Name;
            //Course Modules linked to the couse
            List<Module> listmodules = currentUser.Course.Modules.ToList();
            var allUserDocuments = currentUser.Documents.ToList();
            var userDocsCourseId = currentUser.CourseId;
            var userDocsModuleId = currentUser.Course.Modules;

            var modules = uow.CourseRepository.GetOne(courseId, true).Result.Modules;
            var moduleVms = modules.Select(m => new ModuleDocumentViewModel
            {
                Name = m.Name,
                Documents = m.Documents
            }).ToList();

            var activityVms = new List<ActivityDocumentViewModel>();
            foreach (var item in modules)
            {
                foreach (var act in item.Activities)
                {
                    activityVms.Add(new ActivityDocumentViewModel
                    {
                        Name = act.Name,
                        Documents = act.Documents
                    });
                }
            }

            var model = new UserDocumentViewModel
            {
                DocumentName = allUserDocuments.Select(a => a.Name).ToString(),
                CourseName = course.Name,
                CourseDocuments = uow.CourseRepository.GetOne(courseId, true).Result.Documents,
                ModuleViewModels = moduleVms,
                ActivityViewModels = activityVms,
                LMSUserDocuments = currentUser.Documents.ToList()

            };

            return View(model);
        }

        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Path.Combine(this.environment.WebRootPath) + "/docs/firstName_lastName" + "/" + fileName + ".pdf";
            // string path = Path.Combine(Directory.GetCurrentDirectory(), "/docs/firstName_lastName/", fileName + ".pdf");
            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            //Send the File to Download.

            return File(bytes, "application/octet-stream", fileName);
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
