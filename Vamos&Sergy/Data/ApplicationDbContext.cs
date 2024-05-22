using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vamos_Sergy.Models;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Quest> Quests { get; set; }

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
            builder.Entity<Item>();

            builder.Entity<Equipment>()
                .HasOne(x => x.Owner)
                .WithMany()
                .HasForeignKey(x => x.OwherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Quest>();

            base.OnModelCreating(builder);
        }
    }
}
