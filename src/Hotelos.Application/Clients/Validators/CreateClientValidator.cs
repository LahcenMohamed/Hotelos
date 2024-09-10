using FluentValidation;
using Hotelos.Application.Contracts.Clients.Dtos;

namespace Hotelos.Application.Clients.Validators
{
    public sealed class CreateClientValidator : AbstractValidator<CreateClientDto>
    {
        public CreateClientValidator()
        {
            RuleFor(x => x.FirstName).NotNull()
                                     .NotEmpty()
                                     .MaximumLength(100);

            RuleFor(x => x.MiddleName).MaximumLength(100);

            RuleFor(x => x.LastName).NotNull()
                                     .NotEmpty()
                                     .MaximumLength(100);

            RuleFor(x => x.Email).EmailAddress().MaximumLength(150);
            RuleFor(x => x.PhoneNumber).MaximumLength(150);
        }
    }
}
