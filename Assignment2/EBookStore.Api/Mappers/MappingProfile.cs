using AutoMapper;
using EBookStore.Api.Dtos.Author;
using EBookStore.Api.Models;

namespace EBookStore.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Author, CreateAuthorRequest>().ReverseMap();
        }
    }
}
