using Hotelos.Domain.Hotels;
using Hotelos.Domain.Rooms;
using Hotelos.Domain.Rooms.Entities.Floors;
using Hotelos.Domain.Rooms.Entities.RoomsTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelos.EntityFrameworkCore.Configurations
{
    public sealed class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.CountOfBeds).IsRequired()
                                                .HasDefaultValue(1);

            builder.HasOne<Floor>()
                   .WithMany()
                   .HasForeignKey(x => x.FloorId);

            builder.HasOne<RoomType>()
                   .WithMany()
                   .HasForeignKey(x => x.RoomTypeId);

            builder.HasOne<Hotel>()
                   .WithMany()
                   .HasForeignKey(x => x.HotelId);
        }
    }
}
