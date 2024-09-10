using Hotelos.Application.Contracts.Clients.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Hotelos.Application.Contracts.Clients
{
    public interface IClientService
    {
        Task<GetClientDto> Create(CreateClientDto createClientDto);
        Task<GetClientDto> Update(UpdateClientDto updateClientDto);
        Task Delete(int id);
        Task<PagedResultDto<GetClientDto>> GetAll(PagedResultRequestDto requestDto);
    }
}
