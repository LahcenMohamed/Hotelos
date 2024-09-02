using FluentValidation;
using Hotelos.Application.Contracts.RoomTypes.Dtos;

namespace Hotelos.Application.RoomTypes.Validators
{
    public sealed class UpdateRoomTypeDtoValidator : AbstractValidator<UpdateRoomTypeDto>
    {
        public UpdateRoomTypeDtoValidator()
        {
            RuleFor(x => x.Name).NotNull()
                                .NotEmpty()
                                .MaximumLength(100);

            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
