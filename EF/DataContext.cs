using GeolocationAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GeolocationAPI.EF
{
    public class DataContext : DbContext, IUnitOfWork
    {
        public DbSet<Geolocation> Geolocations { get; set; }

        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Geolocation>(entity =>
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
