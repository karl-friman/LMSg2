using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUnitOfWork
    {
        IAsyncRepository<Course> CourseRepository { get; }
        IAsyncRepository<Module> ModuleRepository { get; }
        IAsyncRepository<Activity> ActivityRepository { get; }

        IAsyncRepository<ActivityType> ActivityTypeRepository { get; }
        IAsyncRepository<Document> DocumentRepository { get; }
        IAsyncRepository<LMSUser> LMSUserRepository { get; }

        Task CompleteAsync();
    }
}
