using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteApplication.Controllers;
using NoteApplication.Core.Configuration;
using NoteApplication.Data;
using NoteApplication.Models;
using NoteApplication.ViewModels;
using System.Reflection;

namespace NoteApplication.IntegrationTesting
{
    public class TestNoteController
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        [Fact]
        public void TestIndex()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);

            IActionResult result;
            using (ApplicationDbContext applicationDbContext = new(optionsBuilder.Options))
            {
                result = new NoteController(applicationDbContext, _userManager, _unitOfWork).Index(null);
            }

            var okResult = Assert.IsType<OkObjectResult>(result);
            var notes = Assert.IsType<List<Note>>(okResult.Value);
            var note = Assert.Single(notes);

            Assert.NotNull(note);
        }

        [Fact]
        public void TestCreateGetMethod()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);

            NoteViewModel noteViewModel = new NoteViewModel();

            IActionResult result;
            using (ApplicationDbContext applicationDbContext = new(optionsBuilder.Options))
            {
                result = new NoteController(applicationDbContext, _userManager, _unitOfWork).Create(noteViewModel);
            }

            var okResult = Assert.IsType<OkObjectResult>(result);
            var notes = Assert.IsType<List<Note>>(okResult.Value);
            var note = Assert.Single(notes);

            Assert.NotNull(note);
        }
    }
}