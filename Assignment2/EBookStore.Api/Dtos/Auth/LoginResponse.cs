namespace EBookStore.Api.Dtos.Auth
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Token { get; set; }
    }
}
