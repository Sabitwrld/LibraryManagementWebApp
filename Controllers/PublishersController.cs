using lLibraryManagementWebApp.Data;
using lLibraryManagementWebApp.Models;
using lLibraryManagementWebApp.ViewModels.BookCategory;
using lLibraryManagementWebApp.ViewModels.Publisher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lLibraryManagementWebApp.Controllers
{
    public class PublishersController : Controller
    {
        private readonly AppDbContext _context;

        public PublishersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var publishers = await _context.Publishers.ToListAsync();
            return View(publishers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PublisherCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var publisher = new Publisher
                {
                    Name = model.Name
                };

                _context.Add(publisher);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Publisher created successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "There was an error creating the Publisher.";
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            var model = new PublisherEditVM
            {
                Id = publisher.Id,
                Name = publisher.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PublisherEditVM model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var publisher = await _context.Publishers.FindAsync(id);
                if (publisher == null)
                {
                    return NotFound();
                }

                publisher.Name = model.Name;

                _context.Update(publisher);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Publisher updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "There was an error updating the Publisher.";
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Publisher deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
