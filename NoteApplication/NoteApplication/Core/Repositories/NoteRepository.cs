using NoteApplication.Core.IRepositories;
using NoteApplication.Data;
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