using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.SubscriptionHotels;
using Hotelos.Application.Contracts.SubscriptionHotels.Dtos;
using Hotelos.Domain.Subscription.Entities.SubscriptionHotels;
using Hotelos.Domain.Subscriptions;
using Hotelos.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.SubscriptionHotels
{
    public class SubscriptionHotelService(IRepository<SubscriptionHotel> subscriptionHotelRepository,
                                          IRepository<Subscription> subscriptionRepository)
        : BaseServices, ISubscriptionHotelService
    {
        private readonly IRepository<SubscriptionHotel> _subscriptionHotelRepository = subscriptionHotelRepository;
        private readonly IRepository<Subscription> _subscriptionRepository = subscriptionRepository;

        [Authorize(HotelosPermissions.CreateSubscriptionHotel)]
        public async Task Create(CreateSubscriptionHotelDto createSubscriptionHotelDto)
        {
            var subscription = await _subscriptionRepository.FirstOrDefaultAsync(x => x.Id == createSubscriptionHotelDto.SubscriptionId);
            var DateNow = DateOnly.FromDateTime(DateTime.Now);
            var DateAfter = DateOnly.FromDateTime(DateTime.Now.AddMonths(subscription.NumberOfMonths));
            var subHotel = SubscriptionHotel.Create(DateNow,
                                                    DateAfter,
                                                    createSubscriptionHotelDto.HotelId,
                                                    createSubscriptionHotelDto.SubscriptionId,
                                                    (Guid)CurrentUser.Id);
            await _subscriptionHotelRepository.InsertAsync(subHotel, true);
        }
    }
}
