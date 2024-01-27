namespace EBookStore.Mvc.Models.Book
{
    public class BookAuthorRequest
    {
        public int AuthorId { get; set; }
        public int? AuthorOrder { get; set; }
        public decimal? RoyaltyPercentage { get; set; }
    }
}
