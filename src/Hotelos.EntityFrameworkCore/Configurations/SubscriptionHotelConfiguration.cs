using Hotelos.Domain.Hotels;
using Hotelos.Domain.Subscription.Entities.SubscriptionHotels;
using Hotelos.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelos.EntityFrameworkCore.Configurations
{
    public sealed class SubscriptionHotelConfiguration : IEntityTypeConfiguration<SubscriptionHotel>
    {
        public void Configure(EntityTypeBuilder<SubscriptionHotel> builder)
        {
            builder.HasOne<Hotel>()
                   .WithMany()
                   .HasForeignKey(x => x.HotelId);

            builder.HasOne<Subscription>()
                   .WithMany()
                   .HasForeignKey(x => x.SubscriptionId);
        }
    }
}
