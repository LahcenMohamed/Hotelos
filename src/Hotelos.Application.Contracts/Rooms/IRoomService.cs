using Hotelos.Application.Contracts.Rooms.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.Rooms
{
    public interface IRoomService
    {
        Task<GetRoomDto> Create(CreateRoomDto createRoomDto);
        Task<GetRoomDto> Update(UpdateRoomDto updateRoomDto);
        Task Delete(int roomId);
        Task<List<GetRoomDto>> GetAll(GetRoomsRequestDto getRoomsRequestDto);
    }
}
