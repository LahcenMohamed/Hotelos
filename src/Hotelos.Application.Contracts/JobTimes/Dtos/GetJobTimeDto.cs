using System;
using Volo.Abp.Application.Dtos;

namespace Hotelos.Application.Contracts.JobTimes.Dtos
{
    public sealed class GetJobTimeDto : EntityDto<int>
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DayOfWeek Day { get; set; }
        public int EmployeeId { get; set; }
    }
}
