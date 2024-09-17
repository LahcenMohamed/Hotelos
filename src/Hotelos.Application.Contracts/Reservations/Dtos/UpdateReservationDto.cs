namespace Hotelos.Application.Contracts.Reservations.Dtos
{
    public sealed record UpdateReservationDto(int Id,
                                              decimal TotalPrice,
                                              decimal RestPrice,
                                              int CountOfPeople,
                                              int ClientId,
                                              int RoomId);
}
