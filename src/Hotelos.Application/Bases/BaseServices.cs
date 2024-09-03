using System.Linq;
using Volo.Abp.Application.Services;

namespace Hotelos.Application.Bases
{
    public class BaseServices : ApplicationService
    {
        protected string ValidationErorrResult(FluentValidation.Results.ValidationResult validationResult)
        {
            var message = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
            return message;
        }
    }
}
