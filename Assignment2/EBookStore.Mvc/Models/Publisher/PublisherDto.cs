namespace EBookStore.Mvc.Models.Publisher
{
    public class PublisherDto
    {
        public int PubId { get; set; }
        public string? PublisherName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }

    public class PublisherResponse
    {
        public List<PublisherDto> Publishers { get; set; }
    }
}
