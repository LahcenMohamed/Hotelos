using Hotelos.Application.Bases;
using Hotelos.Application.Clients.Mappers;
using Hotelos.Application.Clients.Validators;
using Hotelos.Application.Contracts.Clients;
using Hotelos.Application.Contracts.Clients.Dtos;
using Hotelos.Domain.Clients;
using Hotelos.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.Clients
{
    public class ClientsService(IRepository<Client, int> clientRepository,
                                IDistributedCache<List<GetClientDto>> distributedCache) : BaseServices, IClientService
    {
        private readonly IRepository<Client, int> _clientRepository = clientRepository;
        private readonly IDistributedCache<List<GetClientDto>> _distributedCache = distributedCache;

        [Authorize(HotelosPermissions.CreateClient)]
        public async Task<GetClientDto> Create(CreateClientDto createClientDto)
        {
            await ValidationErorrResult(new CreateClientValidator(), createClientDto);

            (var hotelId, var userId) = GetHotelIdAndUserId();

            var client = Client.Create(createClientDto.FirstName,
                                       createClientDto.MiddleName,
                                       createClientDto.LastName,
                                       hotelId,
                                       userId,
                                       createClientDto.Email,
                                       createClientDto.PhoneNumber,
                                       createClientDto.Description);

            await _clientRepository.InsertAsync(client, true);
            var mapper = new GetClientDtoMapper();
            var clientMapping = mapper.ToDto(client);
            return clientMapping;
        }

        [Authorize(HotelosPermissions.GetClients)]
        public async Task<PagedResultDto<GetClientDto>> GetAll(PagedResultRequestDto requestDto)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var lst = await _distributedCache.GetOrAddAsync($"ClientsOfHotel-{hotelId}-skip_{requestDto.SkipCount}-take_{requestDto.MaxResultCount}",
                async () => await GetClientsFromDb(requestDto));
            return new PagedResultDto<GetClientDto>(await ClientsCount(), lst);
        }

        [Authorize(HotelosPermissions.UpdateClient)]
        public async Task<GetClientDto> Update(UpdateClientDto updateClientDto)
        {
            await ValidationErorrResult(new UpdateClientDtoValidator(), updateClientDto);

            (var hotelId, var userId) = GetHotelIdAndUserId();

            var client = await FindAggragateRootAsync(_clientRepository, updateClientDto.Id, hotelId, "Client");

            client.Update(updateClientDto.FirstName,
                          updateClientDto.MiddleName,
                          updateClientDto.LastName,
                          userId,
                          updateClientDto.Email,
                          updateClientDto.PhoneNumber,
                          updateClientDto.Description);

            await _clientRepository.UpdateAsync(client, true);
            var mapper = new GetClientDtoMapper();
            var clientMapping = mapper.ToDto(client);
            return clientMapping;
        }

        [Authorize(HotelosPermissions.DeleteClient)]
        public async Task Delete(int id)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();

            var client = await FindAggragateRootAsync(_clientRepository, id, hotelId, "Client");

            await _clientRepository.DeleteAsync(client, true);
        }


        private async Task<List<GetClientDto>> GetClientsFromDb(PagedResultRequestDto requestDto)
        {
            (var hotelId, var userId) = GetHotelIdAndUserId();
            var clients = await _clientRepository.GetQueryableAsync();
            var mapper = new GetClientDtoMapper();
            return clients.Where(x => x.HotelId == hotelId).Skip(requestDto.SkipCount * requestDto.MaxResultCount).Take(requestDto.MaxResultCount).ToDto().ToList();
        }

        private async Task<int> ClientsCount()
        {
            return await _clientRepository.CountAsync();
        }
    }
}
