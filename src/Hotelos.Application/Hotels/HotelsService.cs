using FluentValidation;
using Hotelos.Application.Bases;
using Hotelos.Application.Contracts.Hotels;
using Hotelos.Application.Contracts.Hotels.Dtos;
using Hotelos.Application.Hotels.Mappers;
using Hotelos.Application.Hotels.Validators;
using Hotelos.Domain.Hotels;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.Hotels
{
    public class HotelsService(IRepository<Hotel, int> hotelRepository) : BaseServices, IHotelsService
    {
        private readonly IRepository<Hotel, int> _hotelRepository = hotelRepository;

        [Authorize]
        public async Task<GetSingleHotelDto> CreateAsync(CreateHotelDto command)
        {
            var validateResult = new CreateHotelValidator().Validate(command);
            if (!validateResult.IsValid)
            {
                var message = ValidationErorrResult(validateResult);
                throw new ValidationException(message);
            }
            var hotel = Hotel.Create(command.Name,
                                     command.State,
                                     command.City,
                                     command.Street,
                                     CurrentUser.PhoneNumber,
                                     CurrentUser.Email,
                                     command.Description,
                                     (Guid)CurrentUser?.Id);
            var isAlreadyExist = await _hotelRepository.AnyAsync(x => x.UserId == CurrentUser.Id);
            if (isAlreadyExist)
            {
                throw new UserFriendlyException("this user has already an hotel");
            }

            var hotelResponse = await _hotelRepository.InsertAsync(hotel, true);
            var mapper = new GetSingleHotelMapper();
            return mapper.ToDto(hotelResponse);
        }

        [Authorize]
        public async Task<GetSingleHotelDto> GetProfileAsync()
        {
            var hotelIdClaim = CurrentUser.FindClaim("hotelId");
            var hotelId = int.Parse(hotelIdClaim.Value);

            var hotel = await _hotelRepository.FirstOrDefaultAsync(x => x.Id == hotelId);
            var mapper = new GetSingleHotelMapper();
            return mapper.ToDto(hotel);
        }
    }
}
