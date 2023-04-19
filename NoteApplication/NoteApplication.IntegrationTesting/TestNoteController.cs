using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteApplication.Controllers;
using NoteApplication.Data;
using NoteApplication.Models;
using System.Reflection;

namespace NoteApplication.IntegrationTesting
{
    public class TestNoteController
    {
        [Fact]
        public void TestIndex()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);

            IActionResult result;
            using (ApplicationDbContext applicationDbContext = new(optionsBuilder.Options))
            {
                result = new NoteController(applicationDbContext, null, null).Index("");
            }

            var okResult = Assert.IsType<OkObjectResult>(result);
            var notes = Assert.IsType<List<Note>>(okResult.Value);
            var note = Assert.Single(notes);

            Assert.NotNull(note);
        }
    }
}