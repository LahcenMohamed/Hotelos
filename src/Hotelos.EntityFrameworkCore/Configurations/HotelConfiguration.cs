using Hotelos.Domain.Hotels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.Identity;

namespace Hotelos.Configurations
{
    public sealed class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.Property(x => x.Name).IsRequired()
                                         .HasMaxLength(100);

            builder.Property(x => x.PhoneNumber).HasMaxLength(15);

            builder.Property(x => x.Email).HasMaxLength(150);

            builder.OwnsOne(x => x.Address, addressBuilder =>
            {
                addressBuilder.Property(x => x.State).IsRequired().HasMaxLength(100);
                addressBuilder.Property(x => x.City).IsRequired().HasMaxLength(100);
                addressBuilder.Property(x => x.Street).IsRequired().HasMaxLength(100);
            });

            builder.HasOne<IdentityUser>()
                   .WithMany()
                   .HasForeignKey(x => x.UserId);
        }
    }
}
