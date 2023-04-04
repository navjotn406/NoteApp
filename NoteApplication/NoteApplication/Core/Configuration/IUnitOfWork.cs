using NoteApplication.Core.IRepositories;

namespace NoteApplication.Core.Configuration
{
    public interface IUnitOfWork
    {
        INoteRepository Note { get; }
        void Save();
    }
}