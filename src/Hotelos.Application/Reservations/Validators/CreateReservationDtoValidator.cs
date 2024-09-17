using FluentValidation;
using Hotelos.Application.Contracts.Reservations.Dtos;

namespace Hotelos.Application.Reservations.Validators
{
    public sealed class CreateReservationDtoValidator : AbstractValidator<CreateReservationDto>
    {
        public CreateReservationDtoValidator()
        {
            RuleFor(x => x.ExitDate).GreaterThan(x => x.EntryDate);
            RuleFor(x => x.TotalPrice).GreaterThanOrEqualTo(0m);
            RuleFor(x => x.RestPrice).GreaterThanOrEqualTo(0m).LessThanOrEqualTo(x => x.TotalPrice);
            RuleFor(x => x.CountOfPeople).GreaterThanOrEqualTo(1);
            RuleFor(x => x.RoomId).GreaterThanOrEqualTo(1);
            RuleFor(x => x.ClientId).GreaterThanOrEqualTo(1);
        }
    }
}
