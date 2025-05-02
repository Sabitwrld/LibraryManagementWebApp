using System.ComponentModel.DataAnnotations;

namespace lLibraryManagementWebApp.ViewModels.Author
{
    public class AuthorEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
