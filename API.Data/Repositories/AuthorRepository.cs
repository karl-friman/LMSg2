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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DbContextAPI db;

        public AuthorRepository(DbContextAPI db)
        {
            this.db = db;
        }

        public async Task<bool> RemoveAsync(int? Id)
        {
            Author author = await db.Authors.FindAsync(Id);
            if (author == null)
            {
                return false;
            }

            db.Authors.Remove(author);
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

        public async Task<IEnumerable<Author>> getAllAuthors(bool include)
        {
            return include ? await db.Authors.Include(a => a.Literatures).ToListAsync() : await db.Authors.ToListAsync();
        }

        public async Task<Author> getAuthor(int? Id)
        {
            return await db.Authors.FirstOrDefaultAsync(a => a.Id == Id);
        }
    }
}
