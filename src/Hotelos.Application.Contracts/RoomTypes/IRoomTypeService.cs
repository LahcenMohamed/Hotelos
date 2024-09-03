using Hotelos.Application.Contracts.RoomTypes.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.RoomTypes
{
    public interface IRoomTypeService
    {
        Task<GetRoomTypeDto> Create(CreateRoomTypeDto createRoomTypeDto);
        Task<GetRoomTypeDto> Update(UpdateRoomTypeDto updateRoomTypeDto);
        Task<string> Delete(int id);
        Task<List<GetRoomTypeDto>> GetAll();
    }
}
