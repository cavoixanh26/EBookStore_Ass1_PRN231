using AutoMapper;
using EBookStore.Api.Dtos.Author;
using EBookStore.Api.IServices;
using EBookStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EBookStore.Api.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly EBookStoreContext context;
        private readonly IMapper mapper;

        public AuthorService(
            EBookStoreContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<AuthorResponse> Authors(AuthorRequest request)
        {
            var authors = await context.Authors
                .Where(x => string.IsNullOrEmpty(request.Keywords)
                            || x.FirstName.Contains(request.Keywords)
                            || x.LastName.Contains(request.Keywords)
                            || x.City.Contains(request.Keywords)
                            || x.Address.Contains(request.Keywords)
                            || x.Phone.Contains(request.Keywords))
                .ToListAsync();

            var authorDtos = mapper.Map<List<AuthorDto>>(authors);
            var response = new AuthorResponse
            {
                Authors = authorDtos
            };

            return response;
        }

        public async Task CreateAuthor(CreateAuthorRequest request)
        {
            var existEmailOfAuthor = await context.Authors.AnyAsync(x => x.EmailAddress.Equals(request.EmailAddress));
            if (existEmailOfAuthor)
            {
                throw new Exception();
            }

            var author = mapper.Map<Author>(request);
            await context.Authors.AddAsync(author);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAuthor(int id)
        {
            var author = await context.Authors.FindAsync(id);
            if (author == null)
            {
                throw new Exception();
            }
            context.Authors.Remove(author);
            await context.SaveChangesAsync();
        }

        public async Task<AuthorDto> GetAuthorById(int id)
        {
            var author = await context.Authors.FindAsync(id);
            if (author == null)
            {
                throw new Exception();
            }

            var authorDto = mapper.Map<AuthorDto>(author);
            return authorDto;
        }

        public async Task UpdateAuthor(int id, CreateAuthorRequest request)
        {
            var author = await context.Authors.FirstOrDefaultAsync(x => x.AuthorId == id);
            if (author == null)
            {
                throw new Exception();
            }
            mapper.Map(request, author);
            context.Entry(author).State= EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
