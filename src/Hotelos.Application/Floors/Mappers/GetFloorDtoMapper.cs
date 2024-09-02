using Hotelos.Application.Contracts.Floors.Dtos;
using Hotelos.Domain.Rooms.Entities.Floors;
using Riok.Mapperly.Abstractions;

namespace Hotelos.Application.Floors.Mappers
{
    [Mapper]
    public partial class GetFloorDtoMapper
    {
        public partial GetFloorDto ToDto(Floor floor);
    }
}
