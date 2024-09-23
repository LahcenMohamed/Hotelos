using Hotelos.Application.Contracts.Subscriptions.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.Subscriptions
{
    public interface ISubscriptionService
    {
        Task<GetSubscriptionDto> Create(CreateSubscriptionDto createSubscriptionDto);
        Task<GetSubscriptionDto> Update(UpdateSubscriptionDto updateSubscriptionDto);
        Task Delete(int id);
        Task<List<GetSubscriptionDto>> GetAll();
    }
}
