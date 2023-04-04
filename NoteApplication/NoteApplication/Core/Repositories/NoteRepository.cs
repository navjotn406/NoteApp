using NoteApplication.Core.IRepositories;
using NoteApplication.Models;

namespace NoteApplication.Core.Repositories
{
    public class NoteRepository : GenericRepository<Note>, INoteRepository
    {
        public NoteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}