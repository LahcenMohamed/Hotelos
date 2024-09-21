using Hotelos.Application.Contracts.Employees.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.Employees
{
    public interface IEmployeeService
    {
        Task<GetEmployeeDto> Create(CreateEmployeeDto createEmployeeDto);
        Task<GetEmployeeDto> Update(UpdateEmployeeDto updateEmployeeDto);
        Task Delete(int id);
        Task<List<GetEmployeeDto>> GetAll();
    }
}
