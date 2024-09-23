using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.Employees;
using Hotelos.Application.Contracts.Employees.Dtos;
using Hotelos.Application.Employees.Mappers;
using Hotelos.Application.Employees.Validator;
using Hotelos.Domain.Employees;
using Hotelos.Domain.Employees.Entities.JobTypes;
using Hotelos.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.Employees
{
    public class EmployeeService(IRepository<Employee> employeeRepository,
                                 IRepository<JobType> jobTypeRepository) : BaseServices, IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository = employeeRepository;
        private readonly IRepository<JobType> _jobTypeRepository = jobTypeRepository;

        [Authorize(HotelosPermissions.CreateEmployee)]
        public async Task<GetEmployeeDto> Create(CreateEmployeeDto createEmployeeDto)
        {
            await ValidationErorrResult(new CreateEmployeeDtoValidator(), createEmployeeDto);
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var jobType = await FindEntityAsync(_jobTypeRepository, createEmployeeDto.JobTypeId, hotelId, "JobType");
            var employee = Employee.Create(createEmployeeDto.FirstName,
                                           createEmployeeDto.MiddleName,
                                           createEmployeeDto.LastName,
                                           createEmployeeDto.PhoneNumber,
                                           createEmployeeDto.Email,
                                           createEmployeeDto.Address,
                                           createEmployeeDto.Salary,
                                           hotelId,
                                           userId,
                                           createEmployeeDto.UserId,
                                           createEmployeeDto.JobTypeId,
                                           createEmployeeDto.Description);
            await _employeeRepository.InsertAsync(employee, true);
            employee.SetJobType(jobType);
            var mapper = new GetEmployeeDtoMapper();
            return mapper.ToDto(employee);
        }

        [Authorize(HotelosPermissions.DeleteEmployee)]
        public async Task Delete(int id)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var employee = await FindAggragateRootAsync(_employeeRepository, id, hotelId, "Employee");
            await _employeeRepository.DeleteAsync(employee, true);
        }

        [Authorize(HotelosPermissions.GetEmployees)]
        public async Task<List<GetEmployeeDto>> GetAll()
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var employees = await _employeeRepository.WithDetailsAsync(x => x.JobType);
            return employees.Where(x => x.HotelId == hotelId).ToDto().ToList();
        }

        [Authorize(HotelosPermissions.UpdateEmployee)]
        public async Task<GetEmployeeDto> Update(UpdateEmployeeDto updateEmployeeDto)
        {
            await ValidationErorrResult(new UpdateEmployeeDtoValidator(), updateEmployeeDto);
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var employee = await FindAggragateRootAsync(_employeeRepository, updateEmployeeDto.Id, hotelId, "Employee");
            var jobType = await FindEntityAsync(_jobTypeRepository, updateEmployeeDto.JobTypeId, hotelId, "JobType");
            employee.Update(updateEmployeeDto.FirstName,
                            updateEmployeeDto.MiddleName,
                            updateEmployeeDto.LastName,
                            updateEmployeeDto.PhoneNumber,
                            updateEmployeeDto.Email,
                            updateEmployeeDto.Address,
                            updateEmployeeDto.Salary,
                            updateEmployeeDto.JobTypeId,
                            userId,
                            updateEmployeeDto.Description);
            await _employeeRepository.UpdateAsync(employee, true);
            employee.SetJobType(jobType);
            var mapper = new GetEmployeeDtoMapper();
            return mapper.ToDto(employee);
        }
    }
}
