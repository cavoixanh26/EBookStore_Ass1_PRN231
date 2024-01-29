namespace EBookStore.Api.Dtos.Book
{
    public class BookRequest
    {
        public string? Keyword { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}
