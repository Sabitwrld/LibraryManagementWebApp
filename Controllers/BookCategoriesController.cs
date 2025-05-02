using lLibraryManagementWebApp.Data;
using lLibraryManagementWebApp.Models;
using lLibraryManagementWebApp.ViewModels.BookCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lLibraryManagementWebApp.Controllers
{
    public class BookCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public BookCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.BookCategories.ToListAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCategoryCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var category = new BookCategory
                {
                    Name = model.Name
                };

                _context.Add(category);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Book Category created successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "There was an error creating the Book Category.";
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.BookCategories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var model = new BookCategoryEditVM
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookCategoryEditVM model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var category = await _context.BookCategories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                category.Name = model.Name;

                _context.Update(category);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Book Category updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "There was an error updating the Book Category.";
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.BookCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.BookCategories.FindAsync(id);
            if (category != null)
            {
                _context.BookCategories.Remove(category);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Book Category deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
