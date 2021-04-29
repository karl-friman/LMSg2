using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Data.Repositories
{
    interface ITestReposit<T>
    {
        Task AddAsync<T>(T module);
        bool Any(int? Id);
        Task<List<T>> GetAllCourses(bool includeAll);
        void Remove<T>(T module);
        Task<bool> SaveAsync();
        void Update<T>(T module);
    }
}