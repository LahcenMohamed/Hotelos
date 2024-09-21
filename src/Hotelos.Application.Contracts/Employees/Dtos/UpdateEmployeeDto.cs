namespace Hotelos.Application.Contracts.Employees.Dtos
{
    public sealed record UpdateEmployeeDto(int Id,
                                           string FirstName,
                                           string? MiddleName,
                                           string LastName,
                                           string PhoneNumber,
                                           string? Email,
                                           string? Address,
                                           decimal Salary,
                                           int JobTypeId,
                                           string? Description);
}
