namespace dotnet8_web_api_petcare.Dtos.Services
{
    public class GetServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
