using Hotelos.Domain.Hotels;
using Hotelos.Domain.Rooms.Entities.Floors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelos.EntityFrameworkCore.Configurations
{
    public sealed class FloorConfiguration : IEntityTypeConfiguration<Floor>
    {
        public void Configure(EntityTypeBuilder<Floor> builder)
        {
            builder.Property(x => x.Name).IsRequired()
                                         .HasMaxLength(100);

            builder.HasOne<Hotel>()
                   .WithMany(x => x.Floors)
                   .HasForeignKey(x => x.HotelId);
        }
    }
}
