using EBookStore.Api.Dtos.Auth;
using EBookStore.Api.IServices;
using EBookStore.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EBookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly EBookStoreContext context;

        public AuthController(
            IAuthService authService,
            EBookStoreContext context)
        {
            this.authService = authService;
            this.context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.EmailAddress == request.EmailAddress && x.Password == request.Password);
            if (user == null)
            {
                return Unauthorized();
            }else
            {
                var token = GenerateJwtToken(user);
                return Ok(token);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0123456789ABCDEF8123456789ABCDEF9123456789ABCDEF6123456789ABCDEF"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.RoleId.ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5069",
                audience: "http://localhost:5200",
                claims: claim,
                expires: DateTime.Now.AddDays(6),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
