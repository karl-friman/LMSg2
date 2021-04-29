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
    class LMSUserRepository : IAsyncRepository<LMSUser>
    {
        private readonly ApplicationDbContext db;
        public LMSUserRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<LMSUser>> GetAll(bool includeAll)
        {
            if (includeAll)
            {
                List<LMSUser> usersList = await db.Users
                               .Include(c => c.Course)
                               .OrderBy(x => x.Email)
                               .ToListAsync();
                return usersList;

            }
            else
            {
                List<LMSUser> usersList = await db.Users.ToListAsync();
                return usersList;
            }
        }
        public async Task<LMSUser> GetOne(int? id, bool includeAll)
        {
            throw new NotImplementedException();
        }
        public async Task<LMSUser> GetOne(string id, bool includeAll)
        {
            if (includeAll)
            {

                return await db.Users
                             .FirstOrDefaultAsync(m => m.Id == id);
            }
            else
            {
                return await db.Users
                            .FirstOrDefaultAsync(m => m.Id == id);
            }
        }
        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;
        }
        public async Task AddAsync(LMSUser user)
        {
            await db.AddAsync(user);
        }
        public void Update(LMSUser user)
        {
            db.Update(user);
        }
        public void Remove(LMSUser user)
        {
            db.Remove(user);
        }
        public bool Any(int? id)
        {
            throw new NotImplementedException();
        }
        public bool Any(string Id)
        {
            return db.Users.Any(m => m.Id == Id);
        }
    }
}