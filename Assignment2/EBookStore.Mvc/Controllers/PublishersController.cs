using EBookStore.Mvc.Models.Publisher;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EBookStore.Mvc.Controllers
{
    public class PublishersController : Controller
    {
        public IActionResult Index()
        {
            var publishers = new PublisherResponse();
            using (var client = new HttpClient())
            {
                var token = Request.Cookies["access_token"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Auth");
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("http://localhost:5069/api/");
                var response = client.GetAsync("Publishers");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readContent = result.Content.ReadAsStringAsync();
                    readContent.Wait();
                    publishers = JsonConvert.DeserializeObject<PublisherResponse>(readContent.Result);
                }else
                {
                    ModelState.AddModelError(string.Empty, "error");
                }
            }
            return View(publishers.Publishers);
        }

        [HttpPost]
        public IActionResult Create(CreatePublisherRequest request)
        {
            using (var client = new HttpClient())
            {
                var token = Request.Cookies["access_token"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Auth");
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("http://localhost:5069/api/Publishers");
                var postTask = client.PostAsJsonAsync("Publishers", request);
                postTask.Wait();
                var result = postTask.Result;   
                return RedirectToAction(nameof(PublishersController.Index));
            }
        }

        [HttpDelete] 
        public IActionResult Delete(int id)
        {
            using(var client = new HttpClient())
            {
                var token = Request.Cookies["access_token"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Auth");
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("http://localhost:5069/api/");
                var response = client.DeleteAsync("Publishers/"+id);
                response.Wait();
                var result= response.Result;
                return RedirectToAction(nameof(PublishersController.Index));
            }
        }


        public IActionResult Edit(int id)
        {
            var publisher = new PublisherDto();
            using (var client = new HttpClient())
            {
                var token = Request.Cookies["access_token"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Auth");
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("http://localhost:5069/api/");
                var response = client.GetAsync("Publishers/" + id);
                response.Wait();
                var result= response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readContent = result.Content.ReadAsStringAsync();
                    readContent.Wait();
                    publisher = JsonConvert.DeserializeObject<PublisherDto>(readContent.Result);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error");
                }
                return View(publisher); 
            }
        }
        [HttpPost]
        public IActionResult Edit(int id, CreatePublisherRequest request)
        {
            using (var client =new HttpClient())
            {
                var token = Request.Cookies["access_token"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Auth");
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("http://localhost:5069/api/");
                var putTask = client.PutAsJsonAsync("Publishers/"+id, request);
                putTask.Wait();
                var result= putTask.Result;
            }
            return RedirectToAction(nameof(PublishersController.Index));
        }

    }
}
