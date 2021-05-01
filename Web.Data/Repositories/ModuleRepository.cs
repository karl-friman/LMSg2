using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Data;

namespace Web.Data.Repositories
{
    class ModuleRepository : IAsyncRepository<Module> //ITestReposit<Course>
    {
        private readonly ApplicationDbContext db;
        public ModuleRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Module>> GetAll(bool includeAll)
        {
            if (includeAll)
            {
                List<Module> moduleList = await db.Modules
                                            .Include(a => a.Activities)
                                            .Include(c => c.Course)
                                            .Include(d => d.Documents)
                                            .OrderBy(x => x.Name)
                                            .ToListAsync();
                return moduleList;

            }
            else
            {
                List<Module> moduleList = await db.Modules.ToListAsync();
                return moduleList;
            }
        }
        public async Task<Module> GetOne(int? id, bool includeAll)
        {
            if (includeAll)
            {
                return await db.Modules
                             .Include(d => d.Documents)
                             .Include(a => a.Activities)
                             .FirstOrDefaultAsync(m => m.Id == id);
            }
            else
            {
                return await db.Modules.FirstOrDefaultAsync(m => m.Id == id);
            }
        }
        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;
        }
        public async Task AddAsync(Module module)
        {
            await db.AddAsync(module);
        }
        public void Update(Module module)
        {
            db.Update(module);
        }
        public void Remove(Module module)
        {
            db.Remove(module);
        }
        public bool Any(int? Id)
        {
            return db.Modules.Any(m => m.Id == Id);
        }

        public Task<Module> GetOne(string Id, bool includeAll)
        {
            throw new NotImplementedException();
        }

        public bool Any(string Id)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<Module>> GetAll()
        {
            return await db.Modules.ToListAsync();
        }
    }
}
