using Hotelos.Domain.Common.Entity;
using Hotelos.Domain.Common.ValueObjects;
using Hotelos.Domain.Employees.Entities.JobTimes;
using Hotelos.Domain.Employees.Entities.JobTypes;
using System;
using System.Collections.Generic;

namespace Hotelos.Domain.Employees
{
    public sealed class Employee : HotelAggragateRootBase
    {
        public FullName FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string? Email { get; private set; }
        public string? Address { get; private set; }
        public decimal Salary { get; private set; }
        public string? Description { get; private set; }
        public int JobTypeId { get; private set; }
        public JobType JobType { get; private set; }
        public List<JobTime> JobTimes { get; private set; }

        private Employee()
        {
        }

        public static Employee Create(string firstName,
                                      string? middleName,
                                      string lastName,
                                      string phoneNumber,
                                      string? email,
                                      string? address,
                                      decimal salary,
                                      int hotelId,
                                      Guid userId,
                                      int jobTypeId,
                                      string? description)
        {
            return new Employee
            {
                FullName = new FullName(firstName, middleName, lastName),
                PhoneNumber = phoneNumber,
                Email = email,
                Address = address,
                Salary = salary,
                Description = description,
                JobTypeId = jobTypeId,
                HotelId = hotelId,
                CreatorId = userId,
                CreationTime = DateTime.Now
            };
        }

        public void Update(string firstName,
                           string? middleName,
                           string lastName,
                           string phoneNumber,
                           string? email,
                           string? address,
                           decimal salary,
                           int jobTypeId,
                           Guid userId,
                           string? description)
        {
            FullName = new FullName(firstName, middleName, lastName);
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            Salary = salary;
            Description = description;
            JobTypeId = jobTypeId;
            LastModifierId = userId;
            LastModificationTime = DateTime.Now;
        }

        public void SetJobType(JobType jobType)
        {
            JobType = jobType;
        }
    }
}
