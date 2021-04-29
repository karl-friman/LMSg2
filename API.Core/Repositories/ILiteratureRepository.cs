using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Entities;

namespace API.Core.Repositories
{
    public interface ILiteratureRepository : IBasicRepositoryFeatures
    {
        Task<IEnumerable<Literature>> getAllLiteratures(bool include);
        Task<Literature> getLiterature(int? Id, bool include);
    }
}
