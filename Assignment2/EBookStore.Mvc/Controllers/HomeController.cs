using EBookStore.Mvc.Models;
using EBookStore.Mvc.Models.Book;
using EBookStore.Mvc.Models.Publisher;
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
            BookResponse bookResponse = null;
            using (var client = new HttpClient())
            {
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