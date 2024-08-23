using Hotelos.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Hotelos.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HotelosController : AbpControllerBase
{
    protected HotelosController()
    {
        LocalizationResource = typeof(HotelosResource);
    }
}
