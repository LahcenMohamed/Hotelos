using FluentValidation;
using Hotelos.Application.Contracts.RoomTypes.Dtos;

namespace Hotelos.Application.RoomTypes.Validators
{
    public sealed class CreateRoomTypeDtoValidator : AbstractValidator<CreateRoomTypeDto>
    {
        public CreateRoomTypeDtoValidator()
        {
            RuleFor(x => x.Name).NotNull()
                                .NotEmpty()
                                .MaximumLength(100);
        }
    }
}
