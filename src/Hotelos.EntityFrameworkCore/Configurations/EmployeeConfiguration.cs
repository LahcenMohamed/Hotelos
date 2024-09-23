using Hotelos.Domain.Employees;
using Hotelos.Domain.Hotels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.Identity;

namespace Hotelos.EntityFrameworkCore.Configurations
{
    public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.OwnsOne(x => x.FullName, fullNameBuilder =>
            {
                fullNameBuilder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
                fullNameBuilder.Property(x => x.MiddleName).HasMaxLength(100);
                fullNameBuilder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            });

            builder.Property(x => x.Email).HasMaxLength(150);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);

            builder.HasOne(x => x.JobType)
                   .WithMany()
                   .HasForeignKey(x => x.JobTypeId);

            builder.HasOne<Hotel>()
                   .WithMany()
                   .HasForeignKey(x => x.HotelId);

            builder.HasOne<IdentityUser>()
                   .WithMany()
                   .HasForeignKey(x => x.UserId);
        }
    }
}
