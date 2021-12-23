using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Administration
{
    public class AdministrationDbContext : IdentityDbContext<IdentityUser>
    {
        public AdministrationDbContext(DbContextOptions<AdministrationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new[]
            {
                new IdentityRole("student"),
                new IdentityRole("teacher"),
                new IdentityRole("admin")
            });
        }
    }
}
