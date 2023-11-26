using Geolocation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Geolocation.Infrastructure
{
    public class DataContext : DbContext, IUnitOfWork
    {
        public DbSet<Geolocalization> Geolocalizations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Language> Languages { get; set; }

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

                entity.Property(e => e.Type).HasConversion<string>();

                entity.HasOne(e => e.Location)
                    .WithOne()
                    .HasForeignKey<Location>("GeolocationId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable(nameof(Location))
                    .HasKey(e => e.Id);

                entity.HasMany(e => e.Languages)
                    .WithOne()
                    .HasForeignKey("LocationId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable(nameof(Language))
                    .HasKey(e => e.Id);
            });
        }
    }
}
