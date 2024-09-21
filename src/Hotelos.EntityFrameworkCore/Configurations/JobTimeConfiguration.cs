using Hotelos.Domain.Employees.Entities.JobTimes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelos.EntityFrameworkCore.Configurations
{
    public sealed class JobTimeConfiguration : IEntityTypeConfiguration<JobTime>
    {
        public void Configure(EntityTypeBuilder<JobTime> builder)
        {
            builder.HasOne(x => x.Employee)
                   .WithMany(x => x.JobTimes)
                   .HasForeignKey(x => x.EmployeeId);
        }
    }
}
