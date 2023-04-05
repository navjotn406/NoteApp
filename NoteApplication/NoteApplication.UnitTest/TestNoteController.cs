using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteApplication.Controllers;
using NoteApplication.Core.Configuration;
using NoteApplication.Data;
using NoteApplication.Models;
using NoteApplication.ViewModels;

namespace NoteApplication.UnitTest
{
    [TestClass]
    public class TestNoteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        [TestMethod]
        public void TestIndex()
        {
            NoteController noteController = new NoteController(_context, _userManager, _unitOfWork);
            var result = noteController.Index("Test Note") as ViewResult;
            var notes = (List<Note>?) result?.ViewData.Model;

            Assert.Equals(notes?.Count, notes?.Count);
        }

        [TestMethod]
        public void TestCreateGetMethod()
        {            
            NoteController noteController = new NoteController(_context, _userManager, _unitOfWork);
            var result = noteController.Create() as ViewResult;

            Assert.AreEqual("Create", result?.ViewName);
        }

        [TestMethod]
        public void TestCreatePostMethod()
        {
            NoteViewModel noteViewModel = new NoteViewModel();

            NoteController noteController = new NoteController(null, null, null);
            var result = noteController.Delete(1) as ViewResult;
            var createdNote = (Note?)result?.ViewData.Model;

            Assert.Equals(new Note(), createdNote);
        }

        [TestMethod]
        public void TestDeleteMethod()
        {
            NoteController noteController = new NoteController(null, null, null);
            var result = noteController.Delete(1) as ViewResult;

            Assert.Equals("Index", result.ViewName);
        }
    }
}