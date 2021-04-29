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
    public class LevelRepository : ILevelRepository
    {
        private readonly DbContextAPI db;

        public LevelRepository(DbContextAPI db)
        {
            this.db = db;
        }

        public async Task<bool> RemoveAsync(int? Id)
        {
            Level level = await db.Levels.FindAsync(Id);
            if (level == null)
            {
                return false;
            }

            db.Levels.Remove(level);
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

        public async Task<IEnumerable<Level>> getAllLevels()
        {
            return await db.Levels.ToListAsync();
        }

        public async Task<Level> getLevel(int? Id)
        {
            return await db.Levels.FirstOrDefaultAsync(l => l.Id == Id);
        }

        public async Task<Level> getLevelByName(string name)
        {
            return await db.Levels.FirstOrDefaultAsync(l => l.Name.Equals(name));
        }
    }
}
