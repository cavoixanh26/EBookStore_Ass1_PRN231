namespace EBookStore.Mvc.Models.Book
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public decimal? Price { get; set; }
        public decimal? Advance { get; set; }
        public decimal? Royalty { get; set; }
        public int? YtdSales { get; set; }
        public string? Notes { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? PublisherName { get; set; }
    }

    public class BookResponse
    {
        public List<BookDto> Books { get; set; }
    }
}
