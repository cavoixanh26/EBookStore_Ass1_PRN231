using AutoMapper;
using EBookStore.Api.Dtos.Author;
using EBookStore.Api.Dtos.Book;
using EBookStore.Api.Dtos.Publisher;
using EBookStore.Api.Models;

namespace EBookStore.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Author, CreateAuthorRequest>().ReverseMap();

            CreateMap<Publisher, PublisherDto>().ReverseMap();
            CreateMap<Publisher, CreatePublisherRequest>().ReverseMap();

            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Pub.PublisherName))
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.BookAuthors.Select(x => x.Author)))
                .ReverseMap();
            CreateMap<Book, CreateBookRequest>().ReverseMap();
            CreateMap<BookAuthor, BookAuthorRequest>().ReverseMap();
        }
    }
}
