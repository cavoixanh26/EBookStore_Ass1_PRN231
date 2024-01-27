using EBookStore.Api.Dtos.Book;
using EBookStore.Api.Dtos.Publisher;
using EBookStore.Api.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<BookResponse>> Get([FromQuery] BookRequest request)
        {
            var response = await bookService.GetList(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var response = await bookService.GetById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateBookRequest request)
        {
            await bookService.Create(request);
            return Ok("Success");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateBookRequest request)
        {
            await bookService.Update(id, request);
            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await bookService.Delete(id);
            return Ok("Success");
        }
    }
}
