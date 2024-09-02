using FluentValidation;
using Hotelos.Application.Contracts.Hotels.Dtos;

namespace Hotelos.Application.Hotels.Validators
{
    public sealed class CreateHotelValidator : AbstractValidator<CreateHotelDto>
    {
        public CreateHotelValidator()
        {
            ApplyRule();
        }

        private void ApplyRule()
        {
            RuleFor(x => x.Name).NotNull()
                                .NotEmpty()
                                .MaximumLength(100);

            RuleFor(x => x.State).NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.City).NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Street).NotNull()
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
