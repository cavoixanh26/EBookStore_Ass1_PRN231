using EBookStore.Api.Dtos.Author;
using EBookStore.Api.Dtos.Publisher;
using EBookStore.Api.IServices;
using EBookStore.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        [HttpGet]
        public async Task<ActionResult<PublisherResponse>> Get([FromQuery] PublisherRequest request)
        {
            var response = await publisherService.GetList(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDto>> GetById(int id)
        {
            var response = await publisherService.GetById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePublisherRequest request)
        {
            await publisherService.Create(request);
            return Ok("Success");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreatePublisherRequest request)
        {
            await publisherService.Update(id, request);
            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await publisherService.Delete(id);
            return Ok("Success");
        }
    }
}
