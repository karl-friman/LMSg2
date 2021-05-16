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
            var userList = await uow.LMSUserRepository.GetAllWithCourseAndModule(includeAll: true);
            var model = mapper.Map<IEnumerable<LMSUserViewModel>>(userList);
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

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( LMSUserViewModel userViewModel)
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
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            //var x = await _context.Users.FirstOrDefaultAsync(e => e.Email.Equals(id));
            var user = await _userManager.FindByIdAsync(id);

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
                catch (DbUpdateConcurrencyException)
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
                return RedirectToAction(nameof(Index));
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
            //var user = await _context.Users.FirstOrDefaultAsync(id);
            var user = await _userManager.FindByIdAsync(id);
            uow.LMSUserRepository.Remove(user);
            await uow.LMSUserRepository.SaveAsync();


            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return uow.LMSUserRepository.Any(id);
        }
    }
}
