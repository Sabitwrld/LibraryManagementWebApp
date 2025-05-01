using lLibraryManagementWebApp.Data;
using lLibraryManagementWebApp.Models;
using lLibraryManagementWebApp.VIewModels.Books;
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
            var viewModel = new BookCreateVM
            {
                BookCategories = await _context.BookCategories
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToListAsync(),
                Publishers = await _context.Publishers
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                    .ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.BookCategories = await _context.BookCategories
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToListAsync();
                viewModel.Publishers = await _context.Publishers
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                    .ToListAsync();

                return View(viewModel);
            }

            var book = new Book
            {
                Title = viewModel.Title,
                BookCategoryId = viewModel.BookCategoryId,
                PublisherId = viewModel.PublisherId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is null) return NotFound();

            var viewModel = new BookCreateVM
            {
                Title = book.Title,
                BookCategoryId = book.BookCategoryId,
                PublisherId = book.PublisherId,
                BookCategories = await _context.BookCategories
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToListAsync(),
                Publishers = await _context.Publishers
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                    .ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookCreateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.BookCategories = await _context.BookCategories
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToListAsync();
                viewModel.Publishers = await _context.Publishers
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                    .ToListAsync();

                return View(viewModel);
            }

            var book = await _context.Books.FindAsync(id);
            if (book is null) return NotFound();

            book.Title = viewModel.Title;
            book.BookCategoryId = viewModel.BookCategoryId;
            book.PublisherId = viewModel.PublisherId;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookCategory)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book is null) return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
