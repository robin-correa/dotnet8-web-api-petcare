using dotnet8_web_api_petcare.Models.Domains;

using Microsoft.EntityFrameworkCore;

namespace dotnet8_web_api_petcare.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Service> Services { get; set; }
    }
}
