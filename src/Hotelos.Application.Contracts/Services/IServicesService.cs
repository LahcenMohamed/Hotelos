using Hotelos.Application.Contracts.Services.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.Services
{
    public interface IServicesService
    {
        Task<GetServiceDto> Create(CreateServiceDto createServiceDto);
        Task<GetServiceDto> Update(UpdateServiceDto updateServiceDto);
        Task Delete(int id);
        Task<List<GetServiceDto>> GetAll();
    }
}
