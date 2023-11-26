using Geolocation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Geolocation.Infrastructure
{
    public class DataContext : DbContext, IUnitOfWork
    {
        public DbSet<Geolocalization> Geolocalizations { get; set; }

        protected readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Geolocalization>(entity =>
            { 
                entity.ToTable(nameof(Geolocation))
                    .HasKey(e => e.Id);

                entity.HasMany(e => e.Locations)
                    .WithOne()
                    .HasForeignKey("GeolocationId");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable(nameof(Location))
                    .HasKey(e => e.Id);

                entity.HasMany(e => e.Languages)
                    .WithOne()
                    .HasForeignKey("LocationId");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable(nameof(Language))
                    .HasKey(e => e.Id);
            });
        }
    }
}
