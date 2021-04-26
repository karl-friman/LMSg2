using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Entities;

namespace API.Core.Repositories
{
    public interface ILiteratureRepository : BasicRepositoryFeatures
    {
        Task<IEnumerable<Literature>> getAllLiteratures();
        Task<Literature> getLiterature(int? Id);
    }
}
