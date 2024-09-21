using Hotelos.Application.Contracts.JobTypes.Dtos;

namespace Hotelos.Application.Contracts.Employees.Dtos
{
    public sealed class GetEmployeeDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public string? Description { get; set; }
        public int JobTypeId { get; set; }
        public GetJobTypeDto JobType { get; set; }
    }
}
