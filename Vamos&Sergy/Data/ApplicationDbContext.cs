using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vamos_Sergy.Models;

namespace Vamos_Sergy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Item> Items { get; set; }

        DbSet<SiteUser> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Hero>()
                .HasOne(x => x.Owner)
                .WithMany()
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Item>()
                .HasOne(x => x.Id)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
