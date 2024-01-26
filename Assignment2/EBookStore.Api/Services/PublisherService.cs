using AutoMapper;
using EBookStore.Api.Dtos.Publisher;
using EBookStore.Api.IServices;
using EBookStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace EBookStore.Api.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly EBookStoreContext context;
        private readonly IMapper mapper;

        public PublisherService(EBookStoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Create(CreatePublisherRequest request)
        {
            var existPublisher = await context.Publishers
                .AnyAsync(x => x.PublisherName.Equals(request.PublisherName));
            if (existPublisher)
            {
                throw new Exception();
            }

            var publisher = mapper.Map<Publisher>(request);
            await context.Publishers.AddAsync(publisher);
            await context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PublisherDto> GetById(int id)
        {
            var publisher = await context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                throw new Exception();
            }

            var publisherDto = mapper.Map<PublisherDto>(publisher);
            return publisherDto;
        }

        public async Task<PublisherResponse> GetList(PublisherRequest request)
        {
            var publishers = await context.Publishers
                .Where(x => string.IsNullOrEmpty(request.Keyword)
                        || x.PublisherName.Contains(request.Keyword)
                        || x.City.Contains(request.Keyword)
                        || x.Country.Contains(request.Keyword))
                .ToListAsync();
            var publisherDtos = mapper.Map<List<PublisherDto>>(publishers);
            var response = new PublisherResponse
            {
                Publishers = publisherDtos,
            };

            return response;
        }

        public async Task Update(int id, CreatePublisherRequest request)
        {
            var publisher = await context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                throw new Exception();
            }

            mapper.Map(request, publisher);
            context.Entry(publisher).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
