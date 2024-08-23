using Hotelos.Localization;
using Volo.Abp.Application.Services;

namespace Hotelos;

/* Inherit your application services from this class.
 */
public abstract class HotelosAppService : ApplicationService
{
    protected HotelosAppService()
    {
        LocalizationResource = typeof(HotelosResource);
    }
}
