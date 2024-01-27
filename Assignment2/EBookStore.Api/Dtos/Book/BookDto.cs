using EBookStore.Api.Dtos.Author;

namespace EBookStore.Api.Dtos.Book
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public decimal? Price { get; set; }
        public int? PubId { get; set; }
        public decimal? Advance { get; set; }
        public decimal? Royalty { get; set; }
        public int? YtdSales { get; set; }
        public string? Notes { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? PublisherName { get; set; }
        public ICollection<AuthorDto>? Authors { get; set; }
    }

}
