using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Repositories;
using API.Data.Data;

namespace API.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextAPI db;

        public UnitOfWork(DbContextAPI db)
        {
            this.db = db;
            AuthorRepository = new AuthorRepository(db);
            LiteratureRepository = new LiteratureRepository(db);
            SubjectRepository = new SubjectRepository(db);
        }

        public IAuthorRepository AuthorRepository { get; }
        public ILiteratureRepository LiteratureRepository { get; }
        public ISubjectRepository SubjectRepository { get; }
        public Task CompleteAsync()
        {
            return db.SaveChangesAsync();
        }
    }
}
