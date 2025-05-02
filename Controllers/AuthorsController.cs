using lLibraryManagementWebApp.Data;
using lLibraryManagementWebApp.Models;
using lLibraryManagementWebApp.ViewModels.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace lLibraryManagementWebApp.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _context.Authors.Include(a => a.AuthorContact).ToListAsync();
            return View(authors);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var author = new Author
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    AuthorContact = new AuthorContact
                    {
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email
                    }
                };

                _context.Authors.Add(author);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Author created successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "There was an error creating the author.";
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _context.Authors
                .Include(a => a.AuthorContact)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            var model = new AuthorEditVM
            {
                Name = author.Name,
                Surname = author.Surname,
                Email = author.AuthorContact?.Email,
                PhoneNumber = author.AuthorContact?.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AuthorEditVM model)
        {
           if (ModelState.IsValid)
            {
                var author = await _context.Authors
                    .Include(a => a.AuthorContact)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (author is null)
                {
                    return NotFound();
                }

                author.Name = model.Name;
                author.Surname = model.Surname;
                author.AuthorContact.Email = model.Email;
                author.AuthorContact.PhoneNumber = model.PhoneNumber;

                _context.Update(author);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var author = await _context.Authors
                .Include(a => a.AuthorContact)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Author deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

    }
}
