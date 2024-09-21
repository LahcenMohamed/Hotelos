using System;

namespace Hotelos.Application.Contracts.JobTimes.Dtos
{
    public sealed record CreateJobTimeDto(TimeOnly StartTime,
                                          TimeOnly EndTime,
                                          DayOfWeek Day,
                                          int EmployeeId);
}
