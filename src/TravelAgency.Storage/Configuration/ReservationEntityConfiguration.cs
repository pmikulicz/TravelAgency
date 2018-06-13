using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Domain.Model;

namespace TravelAgency.Storage.Configuration
{
    public sealed class ReservationEntityConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(r => r.TourId)
                .IsRequired();

            builder.Property(t => t.CreationDate)
                .IsRequired();

            builder.Property(r => r.User)
                .IsRequired();

            builder.Property(r => r.Comment);

            builder.HasOne<Tour>()
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TourId);
        }
    }
}