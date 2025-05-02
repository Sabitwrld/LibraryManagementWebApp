using System.ComponentModel.DataAnnotations;

namespace lLibraryManagementWebApp.ViewModels.Publisher
{
    public class PublisherEditVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
