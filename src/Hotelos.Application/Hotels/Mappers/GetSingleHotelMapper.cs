using Hotelos.Application.Contracts.Hotels.Dtos;
using Hotelos.Domain.Hotels;
using Riok.Mapperly.Abstractions;

namespace Hotelos.Application.Hotels.Mappers
{
    [Mapper]
    public partial class GetSingleHotelMapper
    {
        [MapProperty("Address.State", nameof(GetSingleHotelDto.State))]
        [MapProperty("Address.City", nameof(GetSingleHotelDto.City))]
        [MapProperty("Address.Street", nameof(GetSingleHotelDto.Street))]
        public partial GetSingleHotelDto ToDto(Hotel hotel);
    }
}
