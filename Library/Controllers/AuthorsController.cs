using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly LibraryContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthorController(UserManager<ApplicationUser> userManager,LibraryContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            List<Author> model = _db.Authors.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Author author)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            author.User = currentUser;
            _db.Authors.Add(author);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var thisAuthor = _db.Authors
            .Include(author => author.Books)
            .ThenInclude(join => join.Book)
            .FirstOrDefault(author => author.AuthorId == id);
            ViewBag.Books =  _db.AuthorBook.Where(author => author.BookId == id).ToList();
            return View(thisAuthor);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Author foundAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
            ViewBag.AssociatedBooks = _db.Books.ToList();
            //.Where( book => book.AuthorId == id);
            return View(foundAuthor);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Destroy(int id)
        {
            Author foundAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
            _db.Authors.Remove(foundAuthor);
            List<Book> foundBooks = _db.Books.ToList();
            //.Where(book => book.AuthorId == id).ToList();
            foreach (Book book in foundBooks)
            {
                _db.Books.Remove(book);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            Author foundAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
            return View(foundAuthor);
        }
        [HttpPost]
        public ActionResult Update(Author author)
        {
            _db.Entry(author).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}