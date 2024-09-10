using Hotelos.Application.Contracts.Clients.Dtos;
using Hotelos.Domain.Clients;
using Riok.Mapperly.Abstractions;
using System.Linq;

namespace Hotelos.Application.Clients.Mappers
{
    [Mapper]
    public partial class GetClientDtoMapper
    {
        [MapProperty("FullName.FirstName", nameof(GetClientDto.FirstName))]
        [MapProperty("FullName.MiddleName", nameof(GetClientDto.MiddleName))]
        [MapProperty("FullName.LastName", nameof(GetClientDto.LastName))]
        public partial GetClientDto ToDto(Client client);
    }

    [Mapper]
    public static partial class GetClientsDtoMapper
    {
        public static partial IQueryable<GetClientDto> ToDto(this IQueryable<Client> clients);

        [MapProperty("FullName.FirstName", nameof(GetClientDto.FirstName))]
        [MapProperty("FullName.MiddleName", nameof(GetClientDto.MiddleName))]
        [MapProperty("FullName.LastName", nameof(GetClientDto.LastName))]
        public static partial GetClientDto ToClientDto(Client client);
    }
}
