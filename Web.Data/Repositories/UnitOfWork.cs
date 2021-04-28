using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Data;

namespace Web.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        public ICourseRepository CourseRepository { get; private set; }
        //public IModuleRepository ModuleRepository { get; private set; }
        //IActivityRepository ActivityRepository { get; private set; }
        //IActivityTypeRepository ActivityTypeRepository { get; private set; }
        //IDocumentRepository DocumentRepository { get; private set; }
        //IUserRepository UserRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            CourseRepository = new CourseRepository(db);
            //ModuleRepository = new ModuleRepository(db);
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
