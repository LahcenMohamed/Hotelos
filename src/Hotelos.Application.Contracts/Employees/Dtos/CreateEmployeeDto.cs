namespace Hotelos.Application.Contracts.Employees.Dtos
{
    public sealed record CreateEmployeeDto(string FirstName,
                                           string? MiddleName,
                                           string LastName,
                                           string PhoneNumber,
                                           string? Email,
                                           string? Address,
                                           decimal Salary,
                                           int JobTypeId,
                                           string? Description);
}
