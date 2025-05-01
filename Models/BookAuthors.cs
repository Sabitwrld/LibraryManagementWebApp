using lLibraryManagementWebApp.Models.Common;

namespace lLibraryManagementWebApp.Models
{
    public class BookAuthors : BaseEntity
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
