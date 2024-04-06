using dotnet8_web_api_petcare.Data;
using dotnet8_web_api_petcare.Dtos.Services;
using dotnet8_web_api_petcare.Models.Domains;
using dotnet8_web_api_petcare.Repositories.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace dotnet8_web_api_petcare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IServiceRepository _serviceRepository;

        public ServicesController(AppDbContext appDbContext, IServiceRepository serviceRepository)
        {
            _appDbContext = appDbContext;
            _serviceRepository = serviceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var services = await _serviceRepository.GetAllAsync();

            var serviceCollections = new List<GetServiceDto>();
            foreach (var serviceData in services)
            {
                serviceCollections.Add(new GetServiceDto()
                {
                    Id = serviceData.Id,
                    Name = serviceData.Name,
                    Description = serviceData.Description,
                    MinPrice = serviceData.MinPrice,
                    MaxPrice = serviceData.MaxPrice,
                    Status = serviceData.Status,
                    CreatedAt = serviceData.CreatedAt,
                    UpdatedAt = serviceData.UpdatedAt,
                });
            }
            return Ok(services);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Show([FromRoute] int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            var serviceResource = new GetServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                MinPrice = service.MinPrice,
                MaxPrice = service.MaxPrice,
                Status = service.Status,
                CreatedAt = service.CreatedAt,
                UpdatedAt = service.UpdatedAt,
            };

            return Ok(serviceResource);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceDto request)
        {
            var service = new Service
            {
                Name = request.Name,
                Description = request.Description,
                MinPrice = request.MinPrice,
                MaxPrice = request.MaxPrice,
                Status = request.Status,
            };

            await _appDbContext.Services.AddAsync(service);
            await _appDbContext.SaveChangesAsync();

            var serviceResource = new GetServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                MinPrice = service.MinPrice,
                MaxPrice = service.MaxPrice,
                Status = service.Status,
                CreatedAt = service.CreatedAt,
                UpdatedAt = service.UpdatedAt,
            };

            return CreatedAtAction(nameof(Show), new { id = service.Id }, serviceResource);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateServiceDto request)
        {
            var serviceResource = new Service
            {
                Name = request.Name,
                Description = request.Description,
                MinPrice = request.MinPrice,
                MaxPrice = request.MaxPrice,
                Status = request.Status,
            };

            serviceResource = await _serviceRepository.UpdateByIdAsync(id, serviceResource);

            if (serviceResource == null)
            {
                return NotFound();
            }

            return Ok(serviceResource);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var service = await _serviceRepository.DeleteByIdAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            var serviceResource = new GetServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                MinPrice = service.MinPrice,
                MaxPrice = service.MaxPrice,
                Status = service.Status,
                CreatedAt = service.CreatedAt,
                UpdatedAt = service.UpdatedAt,
            };

            return Ok(serviceResource);
        }
    }
}
