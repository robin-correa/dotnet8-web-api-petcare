using dotnet8_web_api_petcare.Data;
using dotnet8_web_api_petcare.Dtos.Services;

using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            var services = _appDbContext.Services.ToList();

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
        public IActionResult Show([FromRoute] int id)
        {
            var service = _appDbContext.Services.FirstOrDefault(x => x.Id == id);

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
