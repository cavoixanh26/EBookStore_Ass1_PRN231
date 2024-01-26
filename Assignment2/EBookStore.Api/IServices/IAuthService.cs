using EBookStore.Api.Dtos.Auth;

namespace EBookStore.Api.IServices
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }
}
