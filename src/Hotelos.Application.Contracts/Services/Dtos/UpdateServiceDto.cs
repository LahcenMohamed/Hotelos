namespace Hotelos.Application.Contracts.Services.Dtos
{
    public sealed record UpdateServiceDto(int Id, string Name, decimal Price, string? Description);
}
