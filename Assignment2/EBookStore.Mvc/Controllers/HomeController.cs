using EBookStore.Mvc.Models;
using EBookStore.Mvc.Models.Book;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

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
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(apiUrl+"Books"))
                {
                    using (var content = response.Content)
                    {
                        var data= await content.ReadAsStringAsync();
                        var bookResponse = JsonConvert.DeserializeObject<BookResponse>(data);
                        return View(bookResponse);
                    }
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}