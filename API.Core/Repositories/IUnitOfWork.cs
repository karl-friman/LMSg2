using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Repositories
{
    public interface IUnitOfWork
    {
        IAuthorRepository AuthorRepository { get; }
        ILiteratureRepository LiteratureRepository { get; }
        ISubjectRepository SubjectRepository { get; }

        ILevelRepository LevelRepository { get; }

        Task CompleteAsync();

    }
}
