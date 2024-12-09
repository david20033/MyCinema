using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyCinema.Data
{
    public class MyCinemaDBContext : IdentityDbContext<IdentityUser>
    {
        public MyCinemaDBContext(DbContextOptions<MyCinemaDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
