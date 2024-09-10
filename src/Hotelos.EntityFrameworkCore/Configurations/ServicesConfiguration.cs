using Hotelos.Domain.Hotels;
using Hotelos.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelos.EntityFrameworkCore.Configurations
{
    public sealed class ServicesConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);

            builder.Property(x => x.Price).IsRequired();

            builder.HasOne<Hotel>()
                   .WithMany()
                   .HasForeignKey(x => x.HotelId);
        }
    }
}
