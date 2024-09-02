using Hotelos.Application.Contracts.Results;
using Hotelos.Application.Contracts.RoomTypes.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.RoomTypes
{
    public interface IRoomTypeService
    {
        Task<Result<GetRoomTypeDto>> Create(CreateRoomTypeDto createRoomTypeDto);
        Task<Result<GetRoomTypeDto>> Update(UpdateRoomTypeDto updateRoomTypeDto);
        Task<Result<string>> Delete(int id);
        Task<Result<List<GetRoomTypeDto>>> GetAll();
    }
}
