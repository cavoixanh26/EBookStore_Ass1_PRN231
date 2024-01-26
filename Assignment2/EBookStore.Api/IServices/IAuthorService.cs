using EBookStore.Api.Dtos.Author;

namespace EBookStore.Api.IServices
{
    public interface IAuthorService
    {
        Task<AuthorResponse> Authors(AuthorRequest request);
        Task<AuthorDto> GetAuthorById(int id);
        Task CreateAuthor(CreateAuthorRequest request);
        Task UpdateAuthor(int id, CreateAuthorRequest request);
        Task DeleteAuthor(int id);
    }
}
