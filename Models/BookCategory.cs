using lLibraryManagementWebApp.Models.Common;

namespace lLibraryManagementWebApp.Models
{
    public class BookCategory : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
