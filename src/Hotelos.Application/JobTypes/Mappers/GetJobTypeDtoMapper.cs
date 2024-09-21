using Hotelos.Application.Contracts.JobTypes.Dtos;
using Hotelos.Domain.Employees.Entities.JobTypes;
using Riok.Mapperly.Abstractions;
using System.Linq;

namespace Hotelos.Application.JobTypes.Mappers
{
    [Mapper]
    public partial class GetJobTypeDtoMapper
    {
        public partial GetJobTypeDto ToDto(JobType jobType);
    }

    [Mapper]
    public static partial class GetJobTypesDtoMapper
    {
        public static partial IQueryable<GetJobTypeDto> ToDto(this IQueryable<JobType> jobTypes);
    }
}
