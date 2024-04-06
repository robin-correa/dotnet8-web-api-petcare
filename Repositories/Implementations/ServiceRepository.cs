using dotnet8_web_api_petcare.Data;
using dotnet8_web_api_petcare.Models.Domains;
using dotnet8_web_api_petcare.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace dotnet8_web_api_petcare.Repositories.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _appDbContext;

        public ServiceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Service>> GetAllAsync()
        {
            return await _appDbContext.Services.ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _appDbContext.Services.FirstOrDefaultAsync(service => service.Id == id);
        }

        public async Task<Service> CreateAsync(Service service)
        {
            await _appDbContext.Services.AddAsync(service);
            await _appDbContext.SaveChangesAsync();
            return service;
        }

        public async Task<Service?> UpdateByIdAsync(int id, Service service)
        {
            var existingService = await _appDbContext.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (existingService == null)
            {
                return null;
            }

            existingService.Name = service.Name;
            existingService.Description = service.Description;
            existingService.MinPrice = service.MinPrice;
            existingService.MaxPrice = service.MaxPrice;
            existingService.Status = service.Status;

            await _appDbContext.SaveChangesAsync();
            return existingService;
        }

        public async Task<Service?> DeleteByIdAsync(int id)
        {
            var existingService = await _appDbContext.Services.FirstOrDefaultAsync(s => s.Id == id);

            if (existingService == null)
            {
                return null;
            }

            _appDbContext.Services.Remove(existingService);
            await _appDbContext.SaveChangesAsync();
            return existingService;
        }
    }
}
