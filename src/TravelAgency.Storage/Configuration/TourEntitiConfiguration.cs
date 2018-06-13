using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Model;

namespace TravelAgency.Storage.Configuration
{
    public sealed class TourEntitiConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Description)
                .IsRequired();

            builder.Property(t => t.Country)
                .IsRequired();

            builder.Property(t => t.StartDate)
                .IsRequired();

            builder.Property(t => t.EndDate)
                .IsRequired();

            builder.Property(t => t.MaxResrvations)
                .IsRequired();

            builder.HasMany<Reservation>()
                .WithOne(r => r.Tour)
                .HasForeignKey(r => r.TourId);
        }
    }
}