
using EBookStore.Api.Dtos.Publisher;

namespace EBookStore.Api.IServices
{
    public interface IPublisherService
    {
        Task<PublisherResponse> GetList(PublisherRequest request);
        Task<PublisherDto> GetById(int id);
        Task Create(CreatePublisherRequest request);
        Task Update(int id, CreatePublisherRequest request);
        Task Delete(int id);
    }
}
