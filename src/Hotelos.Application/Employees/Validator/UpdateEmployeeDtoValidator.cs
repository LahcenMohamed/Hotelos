using FluentValidation;
using Hotelos.Application.Contracts.Employees.Dtos;

namespace Hotelos.Application.Employees.Validator
{
    public sealed class UpdateEmployeeDtoValidator : AbstractValidator<UpdateEmployeeDto>
    {
        public UpdateEmployeeDtoValidator()
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
            RuleFor(x => x.Salary).GreaterThanOrEqualTo(0);
        }
    }
}
