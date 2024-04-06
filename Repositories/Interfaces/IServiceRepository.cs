using dotnet8_web_api_petcare.Models.Domains;

namespace dotnet8_web_api_petcare.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task<Service> CreateAsync(Service service);
        Task<Service> UpdateByIdAsync(int id, Service service);
        Task<Service> DeleteByIdAsync(int id);
    }
}
