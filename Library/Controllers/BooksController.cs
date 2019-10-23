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
  public class BookController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public BookController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    public async Task<ActionResult> Index()
    {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var currentUser = await _userManager.FindByIdAsync(userId);
        List<Book> userBooks = _db.Books.Include(book => book.Authors).ThenInclude(join => join.Author).ToList();
        System.Console.WriteLine(userBooks);
        // Where(entry => entry.User.Id == currentUser.Id).ToList();
        return View(userBooks);
    }

    public ActionResult Create()
    {
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      ViewBag.Authors = _db.Authors.ToList();
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Book book, int AuthorId)
    {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var currentUser = await _userManager.FindByIdAsync(userId);
        book.User = currentUser;
        _db.Books.Add(book);
        if (AuthorId != 0)
        {
            _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
        }
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisBook = _db.Books
        .Include(book => book.Authors)
        .ThenInclude(join => join.Author)
        .FirstOrDefault(book => book.BookId == id);
        ViewBag.Copys = _db.Copies.Where( copy => copy.BookId == id).ToList();
      return View(thisBook);
    }

    public ActionResult Edit(int id)
    {
        var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
        ViewBag.CategoryId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
        return View(thisBook);
    }

    [HttpPost]
    public ActionResult Edit(Book book, int AuthorId)
    {
        if (AuthorId != 0)
        {
            _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
        }
        _db.Entry(book).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult AddCategory(int id)
    {
        var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
        ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
        return View(thisBook);
    }

    [HttpPost]
    public ActionResult AddCategory(Book book, int AuthorId)
    {
        if (AuthorId != 0)
        {
        _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
        }
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
        var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
        return View(thisBook);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
        _db.Books.Remove(thisBook);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteCategory(int joinId)
    {
        var joinEntry = _db.AuthorBook.FirstOrDefault(entry => entry.AuthorBookId == joinId);
        _db.AuthorBook.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  }
}