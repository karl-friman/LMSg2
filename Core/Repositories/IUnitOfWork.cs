using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; }
        //IModuleRepository ModuleRepository { get; }
        //IActivityRepository ActivityRepository { get; }
        //IActivityTypeRepository ActivityTypeRepository { get; }
        //IDocumentRepository DocumentRepository { get; }
        //IUserRepository UserRepository { get; }

        Task CompleteAsync();
    }
}
