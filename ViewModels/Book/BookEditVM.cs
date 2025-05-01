using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace lLibraryManagementWebApp.VIewModels.Books
{
    public class BookEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int BookCategoryId { get; set; }

        [Required]
        public int PublisherId { get; set; }

        public IEnumerable<SelectListItem> BookCategories { get; set; }
        public IEnumerable<SelectListItem> Publishers { get; set; }
    }
}
