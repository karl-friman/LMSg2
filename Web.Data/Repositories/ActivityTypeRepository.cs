using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Data;

namespace Web.Data.Repositories
{
    class ActivityTypeRepository : IAsyncRepository<ActivityType>
    {
        private readonly ApplicationDbContext db;
        public ActivityTypeRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<ActivityType>> GetAll(bool includeAll)
        {
            if (includeAll)
            {
                List<ActivityType> activityTypeList = await db.ActivityTypes
                                            .OrderBy(x => x.Name)
                                            .ToListAsync();
                return activityTypeList;

            }
            else
            {
                List<ActivityType> courseList = await db.ActivityTypes.ToListAsync();
                return courseList;
            }
        }
        public async Task<ActivityType> GetOne(int? id, bool includeAll)
        {
            if (includeAll)
            {

                return await db.ActivityTypes
                                .FirstOrDefaultAsync(m => m.Id == id);
            }
            else
            {
                return await db.ActivityTypes
                            .FirstOrDefaultAsync(m => m.Id == id);
            }
        }
        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;
        }
        public async Task AddAsync(ActivityType course)
        {
            await db.AddAsync(course);
        }
        public void Update(ActivityType course)
        {
            db.Update(course);
        }
        public void Remove(ActivityType course)
        {
            db.Remove(course);
        }
        public bool Any(int? Id)
        {
            return db.ActivityTypes.Any(m => m.Id == Id);
        }

        public Task<ActivityType> GetOne(string Id, bool includeAll)
        {
            throw new NotImplementedException();
        }

        public bool Any(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
