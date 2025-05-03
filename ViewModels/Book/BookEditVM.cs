using System.ComponentModel.DataAnnotations;

namespace lLibraryManagementWebApp.ViewModels.Book
{
    public class BookEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int? BookCategoryId { get; set; }

        [Required]
        public int? PublisherId { get; set; }
    }
}
