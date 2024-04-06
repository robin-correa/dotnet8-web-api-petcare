using dotnet8_web_api_petcare.Data;

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

            return Ok(service);
        }
    }
}
