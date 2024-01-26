namespace EBookStore.Api.Dtos.Publisher
{
    public class CreatePublisherRequest
    {
        public string? PublisherName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}
