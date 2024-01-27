using EBookStore.Api.Dtos.Book;

namespace EBookStore.Api.IServices
{
    public interface IBookService
    {
        Task<BookResponse> GetList(BookRequest request);
        Task<BookDto> GetById(int id);
        Task Create(CreateBookRequest request);
        Task Update(int id, CreateBookRequest request);
        Task Delete(int id);
    }
}
