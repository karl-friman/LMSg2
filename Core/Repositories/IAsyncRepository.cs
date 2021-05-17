using Core.Entities;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IAsyncRepository<T>
    //public interface IAsyncRepository<T, TInput>
    {

        public Task<List<LMSUser>> GetAllWithCourseAndModule();
        Task<List<T>> GetAllWithCourseAndModule(bool includeAll);
        Task<T> GetOne(int? Id, bool includeAll);
        Task<T> GetOne(string Id, bool includeAll);
        Task<bool> SaveAsync();
        Task AddAsync(T added);
        void Update(T updated);
        //bool Any<TInput>(TInput Id);
        bool Any(int? Id);
        bool Any(string Id);
        void Remove(T removed);
        //Task<List<T>> GetAssignmentForUser(string userId);
    }
}
