using Volo.Abp;

namespace Hotelos.Application.Exceptions
{
    public class UnprocessableEntityException : BusinessException
    {
        public UnprocessableEntityException(string validationErorr)
            : base("Informations was encorrect")
        {
            Code = "UnprocessableEntity";
            Details = validationErorr;
        }
    }
}