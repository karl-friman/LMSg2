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
    class ActivityRepository : IAsyncRepository<Activity>
    {
        private readonly ApplicationDbContext db;
        public ActivityRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<Activity>> GetAll(bool includeAll)
        {
            if (includeAll)
            {
                List<Activity> courseList = await db.Activities
                                            .Include(at => at.ActivityType)
                                            .Include(m => m.Module)
                                            .Include(d => d.Documents)
                                            .OrderBy(x => x.Name)
                                            .ToListAsync();
                return courseList;

            }
            else
            {
                List<Activity> courseList = await db.Activities.ToListAsync();
                return courseList;
            }
        }
        public async Task<Activity> GetOne(int? id, bool includeAll)
        {
            if (includeAll)
            {

                return await db.Activities
                                .Include(a => a.ActivityType)
                                .Include(a => a.Module)
                                .FirstOrDefaultAsync(m => m.Id == id);
            }
            else
            {
                return await db.Activities
                            .FirstOrDefaultAsync(m => m.Id == id);
            }
        }
        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;
        }
        public async Task AddAsync(Activity course)
        {
            await db.AddAsync(course);
        }
        public void Update(Activity course)
        {
            db.Update(course);
        }
        public void Remove(Activity course)
        {
            db.Remove(course);
        }
        public bool Any(int? Id)
        {
            return db.Activities.Any(m => m.Id == Id);
        }

        public Task<Activity> GetOne(string Id, bool includeAll)
        {
            throw new NotImplementedException();
        }

        public bool Any(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Activity>> GetAll()
        {
            return await db.Activities.ToListAsync();
        }

    }
}
