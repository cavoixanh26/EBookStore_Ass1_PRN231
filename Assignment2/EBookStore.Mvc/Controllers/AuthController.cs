﻿using EBookStore.Mvc.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EBookStore.Mvc.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            var token = await CallLoginApi(loginRequest);
            if (!string.IsNullOrEmpty(token))
            {
                Response.Cookies.Append("access_token", token);
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var rolesClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "Role");
                TempData["isRole"] = rolesClaim.Value;
                return RedirectToAction("Index", "Home");
            }
            TempData["isRole"] = string.Empty;
            return Unauthorized();
        }

        private async Task<string> CallLoginApi(LoginRequest loginRequest)
        {
            var token = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5069/api/");
                var postTask = client.PostAsJsonAsync("Auth/Login", loginRequest);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    token = await result.Content.ReadAsStringAsync();
                }
            }
            return token;
        }
    }
}
