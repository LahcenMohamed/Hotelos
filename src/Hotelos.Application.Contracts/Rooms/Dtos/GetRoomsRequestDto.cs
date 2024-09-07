namespace Hotelos.Application.Contracts.Rooms.Dtos
{
    public sealed record GetRoomsRequestDto(int RoomTypeId = 0, int FloorId = 0);
}
