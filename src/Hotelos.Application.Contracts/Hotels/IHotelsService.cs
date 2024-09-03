using Hotelos.Application.Contracts.Hotels.Dtos;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.Hotels
{
    public interface IHotelsService
    {
        Task<GetSingleHotelDto> CreateAsync(CreateHotelDto command);
        Task<GetSingleHotelDto> GetProfileAsync();
    }
}
