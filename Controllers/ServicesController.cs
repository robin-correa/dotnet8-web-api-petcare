using AutoMapper;

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
        private readonly IMapper _mapper;

        public ServicesController(AppDbContext appDbContext, IServiceRepository serviceRepository, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var services = await _serviceRepository.GetAllAsync();

            return Ok(_mapper.Map<List<GetServiceDto>>(services));
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

            return Ok(_mapper.Map<GetServiceDto>(service));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = _mapper.Map<Service>(request);

            await _appDbContext.Services.AddAsync(service);
            await _appDbContext.SaveChangesAsync();

            var serviceResource = _mapper.Map<Service>(service);

            return CreatedAtAction(nameof(Show), new { id = service.Id }, serviceResource);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateServiceDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceResource = _mapper.Map<Service>(request);

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

            return Ok(_mapper.Map<Service>(service));
        }
    }
}
