using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Entities;

namespace API.Core.Repositories
{
    public interface ILevelRepository : IBasicRepositoryFeatures
    {
        Task<IEnumerable<Level>> getAllLevels();
        Task<Level> getLevel(int? Id);
        Task<Level> getLevelByName(string name);
    }
}
