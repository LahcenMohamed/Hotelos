using Hotelos.Application.Contracts.Employees.Dtos;
using Hotelos.Application.JobTypes.Mappers;
using Hotelos.Domain.Employees;
using Riok.Mapperly.Abstractions;
using System.Linq;

namespace Hotelos.Application.Employees.Mappers
{
    [Mapper]
    public partial class GetEmployeeDtoMapper
    {
        [UseMapper]
        private readonly GetJobTypeDtoMapper mapper = new();
        [MapProperty("FullName.FirstName", nameof(GetEmployeeDto.FirstName))]
        [MapProperty("FullName.MiddleName", nameof(GetEmployeeDto.MiddleName))]
        [MapProperty("FullName.LastName", nameof(GetEmployeeDto.LastName))]
        public partial GetEmployeeDto ToDto(Employee employee);
    }

    [Mapper]
    [UseStaticMapper(typeof(GetJobTypesDtoMapper))]
    public static partial class GetEmployeesDtoMapper
    {
        public static partial IQueryable<GetEmployeeDto> ToDto(this IQueryable<Employee> employees);
        [MapProperty("FullName.FirstName", nameof(GetEmployeeDto.FirstName))]
        [MapProperty("FullName.MiddleName", nameof(GetEmployeeDto.MiddleName))]
        [MapProperty("FullName.LastName", nameof(GetEmployeeDto.LastName))]
        private static partial GetEmployeeDto ToClientDto(Employee employee);
    }
}
