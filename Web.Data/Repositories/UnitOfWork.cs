using Core.Entities;
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
        public IAsyncRepository<Course> CourseRepository { get; private set; }
        public IAsyncRepository<Module> ModuleRepository { get; private set; }
        public IAsyncRepository<Activity> ActivityRepository { get; private set; }
        public IAsyncRepository<ActivityType> ActivityTypeRepository { get; private set; }
        public IAsyncRepository<Document> DocumentRepository { get; private set; }
        public IAsyncRepository<LMSUser> LMSUserRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            CourseRepository = new CourseRepository(db);
            ModuleRepository = new ModuleRepository(db);
            ActivityRepository = new ActivityRepository(db);
            ActivityTypeRepository = new ActivityTypeRepository(db);
            DocumentRepository = new DocumentRepository(db);
            LMSUserRepository = new LMSUserRepository(db);
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
