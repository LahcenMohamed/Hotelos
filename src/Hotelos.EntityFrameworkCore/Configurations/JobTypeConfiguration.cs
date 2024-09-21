using Hotelos.Domain.Employees.Entities.JobTypes;
using Hotelos.Domain.Hotels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelos.EntityFrameworkCore.Configurations
{
    public sealed class JobTypeConfiguration : IEntityTypeConfiguration<JobType>
    {
        public void Configure(EntityTypeBuilder<JobType> builder)
        {
            builder.Property(x => x.Title).IsRequired()
                                          .HasMaxLength(100);

            builder.HasOne<Hotel>()
                   .WithMany()
                   .HasForeignKey(x => x.HotelId);
        }
    }
}
