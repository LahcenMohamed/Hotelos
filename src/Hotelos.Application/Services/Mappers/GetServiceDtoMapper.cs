using Hotelos.Application.Contracts.Services.Dtos;
using Hotelos.Domain.Services;
using Riok.Mapperly.Abstractions;
using System.Linq;

namespace Hotelos.Application.Services.Mappers
{
    [Mapper]
    public partial class GetServiceDtoMapper
    {
        public partial GetServiceDto ToDto(Service service);
    }

    [Mapper]
    public static partial class GetServicesDtoMapper
    {
        public static partial IQueryable<GetServiceDto> ToProjection(this IQueryable<Service> services);
    }
}
