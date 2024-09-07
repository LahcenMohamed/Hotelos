using FluentValidation;
using Hotelos.Application.Contracts.Rooms.Dtos;
using Hotelos.Domain.Rooms.Entities.Floors;
using Hotelos.Domain.Rooms.Entities.RoomsTypes;
using Volo.Abp.Domain.Repositories;

namespace Hotelos.Application.Rooms.Validators
{
    public sealed class UpdateRoomDtoValidator : AbstractValidator<UpdateRoomDto>
    {
        private readonly IRepository<RoomType, int> _roomTypeRepository;
        private readonly IRepository<Floor> _floorRepository;

        public UpdateRoomDtoValidator(IRepository<RoomType, int> roomTypeRepository, IRepository<Floor> floorRepository)
        {
            _roomTypeRepository = roomTypeRepository;
            _floorRepository = floorRepository;
            Rule();
            CustomRule();
        }

        private void CustomRule()
        {
            RuleFor(x => x.FloorId).MustAsync(async (key, cts) => await _floorRepository.AnyAsync(x => x.Id == key));
            RuleFor(x => x.RoomTypeId).MustAsync(async (key, cts) => await _roomTypeRepository.AnyAsync(x => x.Id == key));
        }

        private void Rule()
        {
            RuleFor(x => x.Id).NotNull().GreaterThanOrEqualTo(1);
            RuleFor(x => x.Number).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.CountOfBeds).NotNull().GreaterThanOrEqualTo(1);
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.PriceOfOneNight).NotNull().GreaterThan(0m);
            RuleFor(x => x.FloorId).NotNull().GreaterThan(0);
            RuleFor(x => x.RoomTypeId).NotNull().GreaterThan(0);
        }
    }
}
