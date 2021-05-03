using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data.Data;

namespace DevSite.Services
{
    public class ModuleSelectService : IModuleSelectService
    {

        private readonly ApplicationDbContext db;

        public ModuleSelectService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<SelectListItem>> GetModuleAsync()
        {
            return await db.Modules

                          .Select(t => new SelectListItem
                          {
                              Text = t.Name.ToString(),
                              Value = t.Id.ToString()
                          }).Distinct()
                          .ToListAsync();
        }
    }
}
