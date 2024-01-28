using EBookStore.Mvc.Models;
using EBookStore.Mvc.Models.Book;
using EBookStore.Mvc.Models.Publisher;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace EBookStore.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string apiUrl = "http://localhost:5069/api/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            if(User.Identity.IsAuthenticated)
{
                foreach (var claim in User.Claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }
            }
            BookResponse bookResponse = null;
            using (var client = new HttpClient())
            {
                var token = Request.Cookies["access_token"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Auth");
                }
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    var expirationTime = jsonToken.ValidTo;
                    // Kiểm tra vai trò trực tiếp từ claims
                    var rolesClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "role");
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await client.GetAsync(apiUrl+"Books"))
                {
                    using (var content = response.Content)
                    {
                        var data= await content.ReadAsStringAsync();
                        bookResponse = JsonConvert.DeserializeObject<BookResponse>(data);
                    }
                }
                using (var responsePublisher = await client.GetAsync(apiUrl + "Publishers"))
                {
                    using (var content = responsePublisher.Content)
                    {
                        var publishersData = await content.ReadAsStringAsync();
                        var publishers = JsonConvert.DeserializeObject<PublisherResponse>(publishersData);
                        ViewBag.Publishers = publishers.Publishers;
                    }
                }
            }
                        return View(bookResponse);
        }

        [HttpPost]
        public IActionResult Create(CreateBookRequest request)
        {
            using (var client = new HttpClient())
            {
                var token = Request.Cookies["access_token"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Auth");
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("http://localhost:5069/api/Books");
                var postTask = client.PostAsJsonAsync("Books", request);
                postTask.Wait();
                var result = postTask.Result;
                return RedirectToAction(nameof(HomeController.Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}