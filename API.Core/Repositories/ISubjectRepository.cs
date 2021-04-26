using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Entities;

namespace API.Core.Repositories
{
    public interface ISubjectRepository : BasicRepositoryFeatures
    {
        Task<IEnumerable<Subject>> getAllSubjects();
        Task<Subject> getSubjects(int? Id);

    }
}
