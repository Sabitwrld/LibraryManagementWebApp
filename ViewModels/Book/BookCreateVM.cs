using System.ComponentModel.DataAnnotations;

namespace lLibraryManagementWebApp.ViewModels.Book
{
    public class BookCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int BookCategoryId { get; set; }

        [Required]
        public int PublisherId { get; set; }
    }
}
