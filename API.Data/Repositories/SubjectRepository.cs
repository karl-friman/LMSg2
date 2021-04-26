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
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DbContextAPI db;

        public SubjectRepository(DbContextAPI db)
        {
            this.db = db;
        }

        public async Task<bool> RemoveAsync(int? Id)
        {
            Subject subject = await db.Subject.FindAsync(Id);
            if (subject == null)
            {
                return false;
            }

            db.Subject.Remove(subject);
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

        public async Task<IEnumerable<Subject>> getAllSubjects()
        {
            return await db.Subject.ToListAsync();
        }

        public async Task<Subject> getSubjects(int? Id)
        {
            return await db.Subject.FirstOrDefaultAsync(s => s.Id == Id);
        }
    }
}
