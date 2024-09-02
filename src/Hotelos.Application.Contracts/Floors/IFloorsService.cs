using Hotelos.Application.Contracts.Floors.Dtos;
using Hotelos.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotelos.Application.Contracts.Floors
{
    public interface IFloorsService
    {
        Task<Result<GetFloorDto>> Create(CreateFloorDto createFloorDtos);
        Task<Result<GetFloorDto>> Update(UpdateFloorDto updateFloorDto);
        Task<Result<string>> Delete(int id);
        Task<Result<List<GetFloorDto>>> GetAll();
    }
}
