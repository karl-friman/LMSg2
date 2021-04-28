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

namespace DevSite.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<LMSUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UsersController(UserManager<LMSUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            //var x = _context.Users.
            return View(await _context.Users
                              .Include(c=>c.Course)
                               .OrderBy(x => x.Email)
                              .ToListAsync());

            //LMSUser selectedUser = null;

            //List<LMSUser> userList = await _context.Users
            //                                .Include(c => c.Course)
            //                                .ToListAsync();

            //if (selected is not null)
            //{
            //    selectedUser = await _context.Users.FindAsync(selected);
            //}
            //else
            //{
            //    selectedUser = null;
            //}

            //LMSUserViewModel moduleIndexModel = new LMSUserViewModel
            //{
            //    LMSUsers = userList,
            //    SelectedUser = selectedUser
            //};


            //return View(moduleIndexModel);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
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
        public async Task<IActionResult> Create([Bind("Id,Email,UserType,PhoneNumber,FirstName,LastName,DateOfBirth,Avatar,CourseId,Course,Documents")] LMSUser user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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
                    _context.Update(user);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(id);
            var user = await _userManager.FindByIdAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
