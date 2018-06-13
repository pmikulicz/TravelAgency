using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Model;
using TravelAgency.Storage.Configuration;

namespace TravelAgency.Storage
{
    public sealed class TravelAgencyDbContext : DbContext
    {
        public TravelAgencyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tour> Tours { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReservationEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TourEntitiConfiguration());
        }
    }
}