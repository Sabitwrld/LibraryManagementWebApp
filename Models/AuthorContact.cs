using lLibraryManagementWebApp.Models.Common;

namespace lLibraryManagementWebApp.Models
{
    public class AuthorContact : BaseEntity
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
