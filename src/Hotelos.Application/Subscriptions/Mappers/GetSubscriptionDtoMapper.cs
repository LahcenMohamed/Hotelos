using Hotelos.Application.Contracts.Subscriptions.Dtos;
using Hotelos.Domain.Subscriptions;
using Riok.Mapperly.Abstractions;
using System.Linq;

namespace Hotelos.Application.Subscriptions.Mappers
{
    [Mapper]
    public partial class GetSubscriptionDtoMapper
    {
        public partial GetSubscriptionDto ToDto(Subscription subscription);
    }

    [Mapper]
    public static partial class GetSubscriptionsDtoMapper
    {
        public static partial IQueryable<GetSubscriptionDto> ToDto(this IQueryable<Subscription> subscriptions);
    }
}
