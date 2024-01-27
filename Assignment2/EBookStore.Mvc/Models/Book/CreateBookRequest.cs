using System.ComponentModel.DataAnnotations;

namespace EBookStore.Mvc.Models.Book
{
    public class CreateBookRequest
    {
        [Required]
        public string? Title { get; set; }
        public string? Type { get; set; }
        public int? PubId { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }
        public decimal? Advance { get; set; }
        public decimal? Royalty { get; set; }
        public int? YtdSales { get; set; }
        public string? Notes { get; set; }
        public DateTime? PublishedDate { get; set; }
        public List<BookAuthorRequest>? Authors { get; set; }
    }
}
