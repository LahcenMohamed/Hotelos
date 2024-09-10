namespace Hotelos.Application.Contracts.Services.Dtos
{
    public sealed record CreateServiceDto(string Name, decimal Price, string? Description);
}
