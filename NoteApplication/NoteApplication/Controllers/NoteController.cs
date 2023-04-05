using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteApplication.Core.Configuration;
using NoteApplication.Data;
using NoteApplication.Models;
using NoteApplication.ViewModels;

namespace NoteApplication.Controllers
{
    public class NoteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public NoteController(ApplicationDbContext context, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _context = context;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        //note/index
        //note
        [HttpGet]
        public IActionResult Index(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                // var notes = _context.Notes.Where(n => n.UserId == userId).ToList();
                var notes = _unitOfWork.Note.GetAllBy(n => n.UserId == userId
                                                      && (n.Category.Contains(search) || n.Title.Contains(search)));
                return View(notes);
            }
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                // var notes = _context.Notes.Where(n => n.UserId == userId).ToList();
                var notes = _unitOfWork.Note.GetAllBy(n => n.UserId == userId);
                return View(notes);
            }
        }

        //note/create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var note = new Note()
                {
                    Title = model.Title,
                    Category = model.Category,
                    Description = model.Description,
                    Color = model.Color,
                    UserId = userId
                };
                // _context.Notes.Add(note);
                // _context.SaveChanges();
                _unitOfWork.Note.Create(note);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index), "Note");
            }
            return View(model);
        }

        //note/create
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            // var note = _context.Notes.FirstOrDefault(n => n.Id == id);
            var note = _unitOfWork.Note.GetBy(n => n.Id == id);
            if (note.UserId == userId)
            {
                var model = new NoteViewModel()
                {
                    Id = note.Id,
                    Title = note.Title,
                    Category = note.Category,
                    Description = note.Description,
                    CreatedDate = note.CreatedDate,
                    Color = note.Color,
                    UserId = userId
                };

                return View(model);
            }
            else
            {
                return Content("You are not authorized");
            }
        }

        [HttpPost]
        public IActionResult Edit(NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                if (model.UserId == userId)
                {
                    var note = new Note
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Category = model.Category,
                        Description = model.Description,
                        UserId = model.UserId,
                        Color = model.Color,
                        CreatedDate = model.CreatedDate
                    };
                    //_context.Notes.Update(note);
                    //_context.SaveChanges();
                    _unitOfWork.Note.Update(note);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Content("You are not authorized");
                }
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return Content("Note Id is null");
            }
            var userId = _userManager.GetUserId(HttpContext.User);
            // var note = _context.Notes.FirstOrDefault(n => n.Id == id);
            var note = _unitOfWork.Note.GetBy(n => n.Id == id);
            if (note.UserId == userId)
            {
                // _context.Notes.Remove(note);
                // _context.SaveChanges();
                _unitOfWork.Note.Delete(id);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return Content("You are not authorized");
        }
    }
}