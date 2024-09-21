using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hotelos.Domain.Employees.Entities.JobTimes
{
    public sealed class JobTime : FullAuditedEntity<int>
    {
        public TimeOnly StartTime { get; private set; }
        public TimeOnly EndTime { get; private set; }
        public DayOfWeek Day { get; private set; }
        public int EmployeeId { get; private set; }
        public Employee Employee { get; private set; }

        private JobTime()
        {

        }

        public static JobTime Create(TimeOnly startTime,
                                     TimeOnly endTime,
                                     DayOfWeek day,
                                     int employeeId,
                                     Guid userId)
        {
            return new JobTime
            {
                EmployeeId = employeeId,
                StartTime = startTime,
                EndTime = endTime,
                Day = day,
                CreatorId = userId,
                CreationTime = DateTime.Now
            };
        }

        public void Update(TimeOnly startTime,
                           TimeOnly endTime,
                           DayOfWeek day,
                           Guid userId)
        {
            StartTime = startTime;
            Day = day;
            EndTime = endTime;
            LastModificationTime = DateTime.Now;
            LastModifierId = userId;
        }
    }
}
