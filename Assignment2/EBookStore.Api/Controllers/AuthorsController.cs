using EBookStore.Api.Dtos.Author;
using EBookStore.Api.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService authorService;

        public AuthorsController(
            IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<AuthorResponse>> GetAuthors([FromQuery]AuthorRequest request)
        {
            var response = await authorService.Authors(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorDetail(int id)
        {
            var response = await authorService.GetAuthorById(id);
            return Ok(response);
        }


        [HttpPost()]

        public async Task<ActionResult> Create(CreateAuthorRequest request)
        {
            await authorService.CreateAuthor(request);
            return Ok("Success");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateAuthorRequest request)
        {
            await authorService.UpdateAuthor(id, request);
            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await authorService.DeleteAuthor(id);
            return Ok("Success");
        }

    }
}
