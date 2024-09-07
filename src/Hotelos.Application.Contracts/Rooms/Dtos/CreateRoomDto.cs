namespace Hotelos.Application.Contracts.Rooms.Dtos
{
    public sealed record CreateRoomDto(int Number,
                                       string? Name,
                                       int CountOfBeds,
                                       decimal PriceOfOneNight,
                                       string? Description,
                                       int FloorId,
                                       int RoomTypeId);
}
