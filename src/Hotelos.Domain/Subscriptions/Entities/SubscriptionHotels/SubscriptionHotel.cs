using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hotelos.Domain.Subscription.Entities.SubscriptionHotels;

public sealed class SubscriptionHotel : FullAuditedEntity<int>
{
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public int HotelId { get; private set; }
    public int SubscriptionId { get; private set; }

    private SubscriptionHotel()
    {
    }

    public static SubscriptionHotel Create(DateOnly startDate, DateOnly endDate, int hotelId, int subscriptionId, Guid userId)
    {
        var subscriptionUser = new SubscriptionHotel()
        {
            StartDate = startDate,
            EndDate = endDate,
            HotelId = hotelId,
            SubscriptionId = subscriptionId,
            CreatorId = userId,
            CreationTime = DateTime.Now
        };

        return subscriptionUser;
    }



}
