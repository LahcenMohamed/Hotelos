using Hotelos.Application.Contracts.Hotels.Dtos;
using Hotelos.Application.Contracts.Results;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.Hotels
{
    public interface IHotelsService
    {
        Task<Result<GetSingleHotelDto>> CreateAsync(CreateHotelDto command);
        Task<Result<GetSingleHotelDto>> GetProfileAsync();
    }
}
