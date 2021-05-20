//using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
//using Web.Data.Data;
using Core.ViewModels;
using Core.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Http;
using System;
using MimeKit;

namespace DevSite.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly UserManager<LMSUser> _userManager;
        private IWebHostEnvironment environment;
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
            var doucumentList = await uow.DocumentRepository.GetAllWithCourseAndModule(includeAll: true);
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
        /**
         * User can see all documents related to their course created 
         * by an admin and download them.
         * 
         */
        public async Task<IActionResult> StudentFilesView()
        {              
            var userId = _userManager.GetUserId(User);
            var testId = "63537c72-f07f-41ab-ab65-e1ff916f9bf5";
            var currentUser = await uow.LMSUserRepository.GetOne(testId, true);
            string firstname = currentUser.FirstName;
            string lastName = currentUser.LastName;
            Course course = currentUser.Course;
            int courseId = (int) currentUser.CourseId;
            string name = course.Name;
            string description = course.Description;
            var modules = uow.CourseRepository.GetOne(courseId, true).Result.Modules;
            var moduleVms = modules.Select(m => new ModuleDocumentViewModel
                {   
                    Name = m.Name,
                    Documents = m.Documents,
                    FilePath = CreatePath(firstname,lastName),
                    Description = m.Description
                    
            }).ToList();
            
            var activityVms = new List<ActivityDocumentViewModel>();
            foreach (var item in modules)
            {
                foreach (var act in item.Activities)
                {
                    activityVms.Add(new ActivityDocumentViewModel
                    {
                        Name = act.Name,
                        Documents = act.Documents,
                        FilePath = CreatePath(firstname, lastName),
                        Description = act.Description

                    });
                }
            }
            var model = new UserDocumentViewModel
            {
                    CourseName = course.Name,
                    CourseDescription = course.Description,
                    CoursePath = CreatePath(firstname, lastName),
                    CourseDocuments = uow.CourseRepository.GetOne(courseId, false).Result.Documents,
                    ModuleViewModels = moduleVms,
                    ActivityViewModels = activityVms,
                    LMSUserDocuments = currentUser.Documents.ToList()
             };

            return View(model);
                     
        }
     
        public async Task<FileResult> DownloadFileWithFileName(string fileName)
        
        {
            var userId = "63537c72-f07f-41ab-ab65-e1ff916f9bf5";
            var currentUser = await uow.LMSUserRepository.GetOne(userId, false);
            string firstname = currentUser.FirstName;
            string lastName = currentUser.LastName;
            string path = Path.Combine(CreatePath(firstname, lastName), fileName);
            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }


        /**
         * Admin page documents created by admins should be sorted under 
         * courses, modules, activities.
         */
        public async Task<IActionResult> AdminFilesView()
        {
            var allUsers = await uow.LMSUserRepository.GetAllWithCourseAndModule(true);
            var allDocuments = await uow.DocumentRepository.GetAllWithCourseAndModule(true);
            var userDocs = allUsers.Select(a => a.Documents).ToList();
            var allCourses = await uow.CourseRepository.GetAllWithCourseAndModule(true);
            var names = allCourses.Select(name => name.Name).ToList();
            var courseDocuments = allCourses.Select(a => a.Documents).ToList();
            var allModules = await uow.ModuleRepository.GetAllWithCourseAndModule(true);
            var moduleVms = allModules.Select(m => new ModuleDocumentViewModel
            {
                Name = m.Name,
                Documents = m.Documents,
                //FilePath = CreatePath(firstname, lastName),
                FilePath = null,
                Description = m.Description

            }).ToList();

            var activityVms = new List<ActivityDocumentViewModel>();

            foreach (var item in allModules)
            {
                foreach (var act in item.Activities)
                {
                    activityVms.Add(new ActivityDocumentViewModel
                    {
                        Name = act.Name,
                        Documents = act.Documents,
                        FilePath = null,
                        Description = act.Description

                    });
                }
            }
     
            var allUsersDocuments = allUsers.Select(d => d.Documents).ToList();


            var model = new AdminDocumentViewModel
            {
               
                CourseNames = names,
                ModuleViewModels = moduleVms,
                ActivityViewModels = activityVms,
                LMSUserDocuments = allDocuments
        };
            return View(model);
           
        }


        //Create a folder
        public string CreatePath(string firstname, string lastName)
        {

            string uploadsFolder = environment.WebRootPath + "\\docs\\" + firstname + "_" + lastName;

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);

            }

            return uploadsFolder;
        }



        //Start implementing the remove function.

        public async Task<IActionResult> DeleteFileFromFolder(string fileName)
        {

            var userId = "63537c72-f07f-41ab-ab65-e1ff916f9bf5";
            var currentUser = await uow.LMSUserRepository.GetOne(userId, false);
            string firstname = currentUser.FirstName;
            string lastName = currentUser.LastName;

            string strPhysicalFolder = CreatePath(firstname, lastName);

            string strFileFullPath = strPhysicalFolder + fileName;

            if (System.IO.File.Exists(strFileFullPath))
            {
                System.IO.File.Delete(strFileFullPath);
            }
            return Ok();
        }



        // GET: Documents/Create
        public IActionResult CreateAssignmentDoc()
        {
            return View();
        }

        //Create an assignment for student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAssignmentDoc(DocumentToAssigmentViewModel model)
        {
            string uniqueFileName = null;
            string userId = null;
            if (model.AssignmentDoc != null)
            {
                userId = "63537c72-f07f-41ab-ab65-e1ff916f9bf5";
                var currentUser = await uow.LMSUserRepository.GetOne(userId, false);
                string firstname = currentUser.FirstName;
                string lastName = currentUser.LastName;

                uniqueFileName = model.AssignmentDoc.FileName;
                string filePath = Path.Combine(CreatePath(firstname, lastName), uniqueFileName);
                model.AssignmentDoc.CopyTo(new FileStream(filePath, FileMode.Create));
                model.LMSUserId = userId;


            }

            if (DocumentExists(model.Id))
            {
                ModelState.AddModelError("DocumentId", "Document already exists");
            }

            if (ModelState.IsValid)
            {
                var document = mapper.Map<Document>(model);
                await uow.DocumentRepository.AddAsync(document);
                await uow.DocumentRepository.SaveAsync();
                return RedirectToAction(nameof(StudentFilesView));
            }
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
