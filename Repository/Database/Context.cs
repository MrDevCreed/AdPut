using Data.Database.EntityConfigurations;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Database
{
    public class Context : IdentityDbContext
    {
        public Context() { }
        public Context(DbContextOptions options) : base(options)
        {

        }

        #region DataBase Tables

        public DbSet<Category> Categories { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Ad> Ads { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<User> AppUsers { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new AdEntityTypeConfiguration().Configure(builder.Entity<Ad>());
            new AddressEntityTypeConfiguration().Configure(builder.Entity<Address>());
            new CategoryEntityTypeConfiguration().Configure(builder.Entity<Category>());
            new CityEntityTypeConfiguration().Configure(builder.Entity<City>());
            new ImageEntityTypeConfigurtion().Configure(builder.Entity<Image>());
            new TownEntityTypeConfiguration().Configure(builder.Entity<Town>());
            new UserEntityTypeConfiguration().Configure(builder.Entity<User>());
        }
    }
}
