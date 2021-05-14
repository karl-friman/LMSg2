using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Web.Data.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Core.Repositories;
using AutoMapper;
using Core.ViewModels;
using Core.ViewModels.Dashboard.Student;
using Core.Extension;

namespace DevSite.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<LMSUser> _userManager;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public UsersController(UserManager<LMSUser> userManager, IUnitOfWork uow, IMapper mapper)
        {
            _userManager = userManager;
            this.uow = uow;
            this.mapper = mapper;
        }
        // GET: Users
        public async Task<IActionResult> Index()
        {
            var userList = await uow.LMSUserRepository.GetAll(includeAll: true);
            var model = mapper.Map<IEnumerable<LMSUserViewModel>>(userList);
            return View(model);
        }
        // GET: Users
        public async Task<IActionResult> StudentClass()
        {
            var userId = _userManager.GetUserId(User);
            var user = await uow.LMSUserRepository.GetOne(userId, includeAll: false);
            var course = await uow.CourseRepository.GetOne(user.CourseId, includeAll: true);
            var courseMembers = course.Users.ToList();
            var model = mapper.Map<IEnumerable<LMSUserViewModel>>(courseMembers);
            return View(model);
        }
        
        // GET: Users
        public async Task<IActionResult> StudentSchedule()
        {
            var userId = _userManager.GetUserId(User);
            var user = await uow.LMSUserRepository.GetOne(userId, true);
            if (user is null)
            {
                return NotFound();
            }

            var modules = user.Course.Modules;
            
            var activities = modules.SelectMany(a => a.Activities).ToList();

            var model = new DashboardStudentScheduleViewModel
            {
                Course = user.Course,
                FullName = user.FullName,
                Activities = activities

            };

            return View(model);
        }

        // GET: Users
        public async Task<IActionResult> AdminStaff()
        {
            var allUsers = await uow.LMSUserRepository.GetAll(includeAll: false);
            var filteredUsers = allUsers.FindAll(z => z.UserType.ToString() == "Admin");

            var model = mapper.Map<IEnumerable<LMSUserViewModel>>(filteredUsers);
            return View(model);
        }
        // GET: Users
        public async Task<IActionResult> AdminStudents()
        {
            var allUsers = await uow.LMSUserRepository.GetAll(includeAll: false);
            var filteredUsers = allUsers.FindAll(z => z.UserType.ToString() == "Student");

            var model = mapper.Map<IEnumerable<LMSUserViewModel>>(filteredUsers);
            return View(model);
        }
        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await uow.LMSUserRepository.GetOne(id,true);
            if (user == null)
            {
                return NotFound();
            }

            var model = mapper.Map<LMSUserViewModel>(user);
            return View(model);
        }

        // GET: 
        public async Task<IActionResult> AddStudent()
        {
            var courseSelectList = await uow.CourseRepository.GetSelectListItems();
            ViewData["CourseSelectList"] = courseSelectList;
            return View();
        }

        // POST: 
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudent(LMSUserViewModel userViewModel)
        {
             if (UserExists(userViewModel.Id))
            {
                ModelState.AddModelError("LMSUserId", "User already exists");
            }

            if (ModelState.IsValid)
            {
                var user = mapper.Map<LMSUser>(userViewModel);
                await uow.LMSUserRepository.AddAsync(user);
                await uow.LMSUserRepository.SaveAsync();
                return RedirectToAction(nameof(Details),user);
            }

            return View(userViewModel);
            //return View("AdminStudents", userViewModel);
            //return RedirectToAction("AdminStudents");
        }
        // GET: 
        public async Task<IActionResult> AddStaff()
        {
            var courseSelectList = await uow.CourseRepository.GetSelectListItems();
            ViewData["CourseSelectList"] = courseSelectList;
            return View();
        }

        // POST: 
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStaff(LMSUserViewModel userViewModel)
        {
            if (UserExists(userViewModel.Id))
            {
                ModelState.AddModelError("LMSUserId", "User already exists");
            }

            if (ModelState.IsValid)
            {
                var user = mapper.Map<LMSUser>(userViewModel);
                await uow.LMSUserRepository.AddAsync(user);
                await uow.LMSUserRepository.SaveAsync();
                return RedirectToAction("Details", user);
                //return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
            //return View("AdminStaff", userViewModel);
            //return RedirectToAction("AdminStaff");
        }


        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            var courseSelectList = await uow.CourseRepository.GetSelectListItems();
            ViewData["CourseSelectList"] = courseSelectList;
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ConcurrencyStamp,Id,Email,UserType,PhoneNumber,FirstName,LastName,DateOfBirth,Avatar,CourseId,Course,Documents")] LMSUser user)
        {
            if (!id.Equals(user.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    uow.LMSUserRepository.Update(user);
                    await uow.LMSUserRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                { 
                    
                    if (user.Email == "")
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", user);
            }
            var model = mapper.Map<LMSUserViewModel>(user);
            return View(model);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await uow.LMSUserRepository.GetOne(id, includeAll: false);
            if (user == null)
            {
                return NotFound();
            }

            var model = mapper.Map<LMSUserViewModel>(user);
            return View(model);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //var user = await _userManager.FindByIdAsync(id);
            var user = await uow.LMSUserRepository.GetOne(id, includeAll: true);
            var allDocs = user.Documents.ToList();

            foreach (var doc in allDocs)
            {
                uow.DocumentRepository.Remove(doc);
            }

            uow.LMSUserRepository.Remove(user);
            await uow.LMSUserRepository.SaveAsync();

            var userType = user.UserType.ToString(); 

            if (userType == "Student")
            {
                return RedirectToAction(nameof(AdminStudents));
            }
            else
            {
                return RedirectToAction(nameof(AdminStaff));
            }
        }

        private bool UserExists(string id)
        {
            return uow.LMSUserRepository.Any(id);
        }
    }
}
