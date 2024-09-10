using Volo.Abp.Application.Dtos;

namespace Hotelos.Application.Contracts.Clients.Dtos
{
    public sealed class CreateClientDto : EntityDto
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
    }
}
