using System;

namespace Hotelos.Application.Contracts.JobTimes.Dtos
{
    public sealed record UpdateJobTimeDto(int Id,
                                          TimeOnly StartTime,
                                          TimeOnly EndTime,
                                          DayOfWeek Day,
                                          int EmployeeId);
}
