using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Entities;
using API.Core.Repositories;
using API.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class LiteratureRepository : ILiteratureRepository
    {
        private readonly DbContextAPI db;

        public LiteratureRepository(DbContextAPI db)
        {
            this.db = db;
        }

        public async Task<bool> RemoveAsync(int? Id)
        {
            Literature literature = await db.Literature.FindAsync(Id);
            if (literature == null)
            {
                return false;
            }

            db.Literature.Remove(literature);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;
        }

        public async Task AddAsync<T>(T added)
        {
            await db.AddAsync(added);
        }

        public async Task<IEnumerable<Literature>> getAllLiteratures()
        {
            return await db.Literature.ToListAsync();
        }

        public async Task<Literature> getLiterature(int? Id)
        {
            return await db.Literature.FirstOrDefaultAsync(l => l.Id == Id);
        }
    }
}
