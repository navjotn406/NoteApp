using Microsoft.AspNetCore.Identity;

namespace NoteApplication.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Note> Notes { get; set; }
    }
}