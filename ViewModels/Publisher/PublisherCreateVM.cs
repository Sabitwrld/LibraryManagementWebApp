using System.ComponentModel.DataAnnotations;

namespace lLibraryManagementWebApp.ViewModels.Publisher
{
    public class PublisherCreateVM
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
