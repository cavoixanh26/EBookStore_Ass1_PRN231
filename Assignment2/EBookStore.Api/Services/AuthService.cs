using EBookStore.Api.Dtos.Auth;
using EBookStore.Api.IServices;
using EBookStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace EBookStore.Api.Services
{
    public class AuthService : IAuthService
    {
        public Task<LoginResponse> Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
