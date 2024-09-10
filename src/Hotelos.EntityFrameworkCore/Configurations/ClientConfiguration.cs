using Hotelos.Domain.Clients;
using Hotelos.Domain.Hotels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelos.EntityFrameworkCore.Configurations
{
    public sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.OwnsOne(x => x.FullName, fullNameBuilder =>
            {
                fullNameBuilder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
                fullNameBuilder.Property(x => x.MiddleName).HasMaxLength(100);
                fullNameBuilder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            });

            builder.Property(x => x.Email).HasMaxLength(150);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);

            builder.HasOne<Hotel>()
                   .WithMany()
                   .HasForeignKey(x => x.HotelId);
        }
    }
}
