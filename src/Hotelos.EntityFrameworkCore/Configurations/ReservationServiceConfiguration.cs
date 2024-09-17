using Hotelos.Domain.Reservations;
using Hotelos.Domain.Reservations.ReservationServices;
using Hotelos.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelos.EntityFrameworkCore.Configurations
{
    public sealed class ReservationServiceConfiguration : IEntityTypeConfiguration<ReservationService>
    {
        public void Configure(EntityTypeBuilder<ReservationService> builder)
        {
            builder.HasOne<Reservation>()
                   .WithMany()
                   .HasForeignKey(e => e.ReservationId);

            builder.HasOne<Service>()
                   .WithMany()
                   .HasForeignKey(e => e.ServiceId);
        }
    }
}
