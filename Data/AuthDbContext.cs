using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet8_web_api_petcare.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var superAdminRoleId = "904eef55-2b91-4f24-b981-0adb2e32f324";
            var adminRoleId = "8ddfa3ad-0986-4a97-a4fd-12bb37da4818";
            var veterinarianRoleId = "49ff8e9d-60cb-4908-a569-4a40e2fac1cf";
            var customerRoleId = "a16441a2-5d37-4f9b-ab49-256445a93fb0";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId,
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin".ToUpper()

                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()

                },
                new IdentityRole
                {
                    Id = veterinarianRoleId,
                    ConcurrencyStamp = veterinarianRoleId,
                    Name = "Veterinarian",
                    NormalizedName = "Veterinarian".ToUpper()

                },
                new IdentityRole
                {
                    Id = customerRoleId,
                    ConcurrencyStamp = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "Customer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
