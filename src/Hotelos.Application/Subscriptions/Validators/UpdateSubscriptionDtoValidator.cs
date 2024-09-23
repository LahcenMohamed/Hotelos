using FluentValidation;
using Hotelos.Application.Contracts.Subscriptions.Dtos;

namespace Hotelos.Application.Subscriptions.Validators
{
    public sealed class UpdateSubscriptionDtoValidator : AbstractValidator<UpdateSubscriptionDto>
    {
        public UpdateSubscriptionDtoValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.NumberOfMounths).GreaterThan(0);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        }
    }
}
