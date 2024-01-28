using EBookStore.Mvc.Models.Book;
using EBookStore.Mvc.Models.Publisher;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace EBookStore.Mvc.Controllers
{
    public class BooksController : Controller
    {
        public async Task<ActionResult> Detail(int id)
        {
            var book = new BookDto();
            using (var client = new HttpClient())
            {
                var token = Request.Cookies["access_token"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Auth");
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("http://localhost:5069/api/");
                var response = client.GetAsync("Books/" + id);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readContent = result.Content.ReadAsStringAsync();
                    readContent.Wait();
                    book = JsonConvert.DeserializeObject<BookDto>(readContent.Result);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error");
                }

                using (var responsePublisher = await client.GetAsync("Publishers"))
                {
                    using (var content = responsePublisher.Content)
                    {
                        var publishersData = await content.ReadAsStringAsync();
                        var publishers = JsonConvert.DeserializeObject<PublisherResponse>(publishersData);
                        ViewBag.Publishers = publishers.Publishers;
                    }
                }
                return View(book);
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, CreateBookRequest request)
        {
            using (var client = new HttpClient())
            {
                var token = Request.Cookies["access_token"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Auth");
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("http://localhost:5069/api/");
                var putTask = client.PutAsJsonAsync("Books/" + id, request);
                putTask.Wait();
                var result = putTask.Result;
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                var token = Request.Cookies["access_token"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Auth");
                }
                
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("http://localhost:5069/api/");
                var response = client.DeleteAsync("Books/" + id);
                response.Wait();
                var result = response.Result;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
