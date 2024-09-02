namespace Hotelos.Application.Contracts.Hotels.Dtos
{
    public sealed record CreateHotelDto(string Name,
                                            string State,
                                            string City,
                                            string Street,
                                            string? Description);
}
