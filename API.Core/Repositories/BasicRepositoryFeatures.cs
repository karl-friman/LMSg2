using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Repositories
{
    public interface BasicRepositoryFeatures
    {
        Task<bool> RemoveAsync(int? Id);
        Task<bool> SaveAsync();
        Task AddAsync<T>(T added);
    }
}
