namespace NoteApplication.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string UserId { get; set; }
        public string Category { get; set; }
        public DateTime CreatedDate { get; set; }

        public User User { get; set; }
    }
}