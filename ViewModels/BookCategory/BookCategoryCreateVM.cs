using System.ComponentModel.DataAnnotations;

namespace lLibraryManagementWebApp.ViewModels.BookCategory
{
    public class BookCategoryCreateVM
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
