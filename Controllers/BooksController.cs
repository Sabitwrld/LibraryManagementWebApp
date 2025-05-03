using lLibraryManagementWebApp.Data;
using lLibraryManagementWebApp.Models;
using lLibraryManagementWebApp.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace lLibraryManagementWebApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(b => b.BookCategory)
                .Include(b => b.Publisher)
                .ToListAsync();

            return View(books);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["BookCategories"] = new SelectList(_context.BookCategories, "Id", "Name");
            ViewData["Publishers"] = new SelectList(_context.Publishers, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = model.Title,
                    BookCategoryId = model.BookCategoryId,
                    PublisherId = model.PublisherId
                };

                _context.Add(book);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Book created successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "There was an error creating the book.";
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookCategory)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var model = new BookEditVM
            {
                Id = book.Id,
                Title = book.Title,
                BookCategoryId = book.BookCategoryId,
                PublisherId = book.PublisherId
            };

            ViewData["BookCategories"] = new SelectList(_context.BookCategories, "Id", "Name", book.BookCategoryId);
            ViewData["Publishers"] = new SelectList(_context.Publishers, "Id", "Name", book.PublisherId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookEditVM model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                book.Title = model.Title;
                book.BookCategoryId = model.BookCategoryId;
                book.PublisherId = model.PublisherId;

                _context.Update(book);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Book updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "There was an error updating the book.";
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookCategory)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Book deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
