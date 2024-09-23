using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.Subscriptions;
using Hotelos.Application.Contracts.Subscriptions.Dtos;
using Hotelos.Application.Subscriptions.Mappers;
using Hotelos.Application.Subscriptions.Validators;
using Hotelos.Domain.Subscriptions;
using Hotelos.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.Subscriptions
{
    public class SubscriptionService(IRepository<Subscription> subscriptionRepository)
        : BaseServices, ISubscriptionService
    {
        private readonly IRepository<Subscription> _subscriptionRepository = subscriptionRepository;

        [Authorize(HotelosPermissions.CreateSubscription)]
        public async Task<GetSubscriptionDto> Create(CreateSubscriptionDto createSubscriptionDto)
        {
            await ValidationErorrResult(new CreateSubscriptionDtoValidator(), createSubscriptionDto);
            Guid userId = (Guid)CurrentUser.Id;
            var subscription = Subscription.Create(createSubscriptionDto.Title,
                                                   createSubscriptionDto.Price,
                                                   createSubscriptionDto.NumberOfMounths,
                                                   userId);
            await _subscriptionRepository.InsertAsync(subscription, true);
            var mapper = new GetSubscriptionDtoMapper();
            return mapper.ToDto(subscription);
        }

        [Authorize(HotelosPermissions.DeleteSubscription)]
        public async Task Delete(int id)
        {
            var subscription = await _subscriptionRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (subscription is null)
            {
                throw new EntityNotFoundException();
            }
            await _subscriptionRepository.DeleteAsync(subscription, true);
        }

        [Authorize(HotelosPermissions.GetSubscriptions)]
        public async Task<List<GetSubscriptionDto>> GetAll()
        {
            var subs = await _subscriptionRepository.GetQueryableAsync();
            return subs.ToDto().ToList();
        }

        [Authorize(HotelosPermissions.UpdateSubscription)]
        public async Task<GetSubscriptionDto> Update(UpdateSubscriptionDto updateSubscriptionDto)
        {
            await ValidationErorrResult(new UpdateSubscriptionDtoValidator(), updateSubscriptionDto);
            Guid userId = (Guid)CurrentUser.Id;
            var subscription = await _subscriptionRepository.FirstOrDefaultAsync(x => x.Id == updateSubscriptionDto.Id);
            if (subscription is null)
            {
                throw new EntityNotFoundException();
            }
            subscription.Update(updateSubscriptionDto.Title,
                                updateSubscriptionDto.Price,
                                updateSubscriptionDto.NumberOfMounths,
                                userId);
            await _subscriptionRepository.UpdateAsync(subscription, true);
            var mapper = new GetSubscriptionDtoMapper();
            return mapper.ToDto(subscription);
        }
    }
}
