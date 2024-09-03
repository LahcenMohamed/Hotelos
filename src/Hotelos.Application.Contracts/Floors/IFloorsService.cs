using Hotelos.Application.Contracts.Floors.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.Floors
{
    public interface IFloorsService
    {
        Task<GetFloorDto> Create(CreateFloorDto createFloorDtos);
        Task<GetFloorDto> Update(UpdateFloorDto updateFloorDto);
        Task<string> Delete(int id);
        Task<List<GetFloorDto>> GetAll();
    }
}
