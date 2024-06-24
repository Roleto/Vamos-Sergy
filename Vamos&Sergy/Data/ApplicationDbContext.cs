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
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Monster> Monsters { get; set; }

        DbSet<SiteUser> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Hero>(hero => hero
                .HasOne(hero => hero.Owner)
                .WithMany(user => user.Heroes)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Cascade));

            builder.Entity<Item>(item => item
            .HasOne(item => item.Equipment)
            .WithOne(eq => eq.Item));

            builder.Entity<Equipment>(eq => eq
                .HasOne(eq => eq.Owner)
                .WithMany(hero => hero.Equipments)
                .HasForeignKey(eq => eq.OwherId)
                .OnDelete(DeleteBehavior.Cascade));

            builder.Entity<Shop>(shop => shop
               .HasOne(shop => shop.Owner)
               .WithMany(hero => hero.Shops)
               .HasForeignKey(shop => shop.OwnerId)
               .OnDelete(DeleteBehavior.Cascade));

            builder.Entity<Quest>();

            builder.Entity<Monster>();

            base.OnModelCreating(builder);
        }
    }
}
