using System.ComponentModel.DataAnnotations;

namespace dotnet8_web_api_petcare.Dtos.Services
{
    public class CreateServiceDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "MinPrice must be a required")]
        public int? MinPrice { get; set; }

        [Required(ErrorMessage = "MaxPrice must be a required")]
        public int? MaxPrice { get; set; }

        [Required(ErrorMessage = "Status must be a required")]
        public int? Status { get; set; }
    }
}
