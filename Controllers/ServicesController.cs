using dotnet8_web_api_petcare.Data;
using dotnet8_web_api_petcare.Dtos.Services;
using dotnet8_web_api_petcare.Models.Domains;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet8_web_api_petcare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ServicesController(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var services = await _appDbContext.Services.ToListAsync();

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
            var service = await _appDbContext.Services.FirstOrDefaultAsync(x => x.Id == id);

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
            var service = await _appDbContext.Services.FirstOrDefaultAsync(x => x.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            service.Name = request.Name;
            service.Description = request.Description;
            service.MinPrice = request.MinPrice;
            service.MaxPrice = request.MaxPrice;
            service.Status = request.Status;

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

            return Ok(serviceResource);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var service = await _appDbContext.Services.FirstOrDefaultAsync(x => x.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            _appDbContext.Services.Remove(service);
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

            return Ok(serviceResource);
        }
    }
}
