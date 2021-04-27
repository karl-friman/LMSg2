using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Entities;

namespace API.Core.Repositories
{
    public interface IAuthorRepository : BasicRepositoryFeatures
    {
        Task<IEnumerable<Author>> getAllAuthors(bool include);
        Task<Author> getAuthor(int? Id);
    }
}
