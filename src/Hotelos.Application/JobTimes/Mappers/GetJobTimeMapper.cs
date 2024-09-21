using Hotelos.Application.Contracts.JobTimes.Dtos;
using Hotelos.Domain.Employees.Entities.JobTimes;
using Riok.Mapperly.Abstractions;
using System.Linq;

namespace Hotelos.Application.JobTimes.Mappers
{
    [Mapper]
    public partial class GetJobTimeMapper
    {
        public partial GetJobTimeDto ToDto(JobTime jobTime);
    }

    [Mapper]
    public static partial class GetJobTimesMapper
    {
        public static partial IQueryable<GetJobTimeDto> ToDto(this IQueryable<JobTime> jobTimes);
    }
}
