using FluentValidation;
using Hotelos.Application.Contracts.Subscriptions.Dtos;

namespace Hotelos.Application.Subscriptions.Validators
{
    public sealed class CreateSubscriptionDtoValidator : AbstractValidator<CreateSubscriptionDto>
    {
        public CreateSubscriptionDtoValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.NumberOfMounths).GreaterThan(0);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        }
    }
}
