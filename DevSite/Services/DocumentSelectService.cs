using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data.Data;

namespace DevSite.Services
{
    public class DocumentSelectService : IDocumentSelectService
    {

        private readonly ApplicationDbContext db;

        public DocumentSelectService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<SelectListItem>> GetDocumentAsync()
        {
            return await db.Activities

                          .Select(t => new SelectListItem
                          {
                              Text = t.Name.ToString(),
                              Value = t.Id.ToString()
                          }).Distinct()
                          .ToListAsync();
        }
    }
}
