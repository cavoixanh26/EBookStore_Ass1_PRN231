using System.ComponentModel.DataAnnotations;

namespace EBookStore.Mvc.Models.Publisher
{
    public class CreatePublisherRequest
    {
        [Required]
        public string? PublisherName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}
