using NoteApplication.Core.Configuration;
using NoteApplication.Core.IRepositories;
using NoteApplication.Core.Repositories;

namespace NoteApplication.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public INoteRepository Note { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Note = new NoteRepository(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}