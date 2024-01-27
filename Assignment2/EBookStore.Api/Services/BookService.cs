using AutoMapper;
using EBookStore.Api.Dtos.Book;
using EBookStore.Api.IServices;
using EBookStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EBookStore.Api.Services
{
    public class BookService : IBookService
    {
        private readonly EBookStoreContext context;
        private readonly IMapper mapper;

        public BookService(EBookStoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Create(CreateBookRequest request)
        {
            var existBook = await context.Books
                .AnyAsync(x => x.Title.Equals(request.Title)
                            && x.PubId == request.PubId
                            && x.PublishedDate == request.PublishedDate);
            var existPublisher = context.Publishers.Any(x => x.PubId == request.PubId);

            if (existBook || !existPublisher)
            {
                throw new Exception();
            }
            var book = mapper.Map<Book>(request);
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    await context.Books.AddAsync(book);
                    await context.SaveChangesAsync();
                    if (request.Authors.Any())
                    {
                        var bookAuthors = new List<BookAuthor>();
                        foreach (var author in request.Authors)
                        {
                            var bookAuthor = mapper.Map<BookAuthor>(author);
                            bookAuthor.BookId = book.BookId;
                            bookAuthors.Add(bookAuthor);
                        }
                        await context.BookAuthors.AddRangeAsync(bookAuthors);
                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.ToString());
                }
            }
        }

        public async Task Delete(int id)
        {
            var book = await context.Books.FindAsync(id);
            if (book == null) { throw new Exception(); }
            var bookAuthors = await context.BookAuthors.Where(x => x.BookId == id).ToListAsync();
            context.BookAuthors.RemoveRange(bookAuthors);
            context.Books.Remove(book);
            await context.SaveChangesAsync();
        }

        public async Task<BookDto> GetById(int id)
        {
            var book = await context.Books
                .Include(x => x.Pub)
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .FirstOrDefaultAsync(x => x.BookId == id);
            var bookDto = mapper.Map<BookDto>(book);
            return bookDto;
        }

        public async Task<BookResponse> GetList(BookRequest request)
        {
            var books = await context.Books
                .Where(x => string.IsNullOrEmpty(request.Keyword)
                        || x.Title.Contains(request.Keyword)
                        || x.Type.Contains(request.Keyword)
                        || x.Pub.PublisherName.Contains(request.Keyword))
                .Include(x => x.Pub)
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .ToListAsync();
            var bookDtos = mapper.Map<List<BookDto>>(books);
            var response = new BookResponse
            {
                Books = bookDtos,
            };

            return response;
        }

        public Task Update(int id, CreateBookRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
