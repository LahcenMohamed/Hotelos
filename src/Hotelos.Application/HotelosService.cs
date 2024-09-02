using Hotelos.Localization;
using Volo.Abp.Application.Services;

namespace Hotelos;

/* Inherit your application services from this class.
 */
public abstract class HotelosService : ApplicationService
{
    protected HotelosService()
    {
        LocalizationResource = typeof(HotelosResource);
    }
}
