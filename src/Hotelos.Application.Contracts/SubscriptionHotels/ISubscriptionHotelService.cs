using Hotelos.Application.Contracts.SubscriptionHotels.Dtos;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.SubscriptionHotels
{
    public interface ISubscriptionHotelService
    {
        Task Create(CreateSubscriptionHotelDto createSubscriptionHotelDto);
    }
}
