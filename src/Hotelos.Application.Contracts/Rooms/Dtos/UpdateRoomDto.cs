namespace Hotelos.Application.Contracts.Rooms.Dtos
{
    public sealed record UpdateRoomDto(int Id,
                                       int Number,
                                       string? Name,
                                       int CountOfBeds,
                                       bool IsVacant,
                                       decimal PriceOfOneNight,
                                       string? Description,
                                       int FloorId,
                                       int RoomTypeId);
}
