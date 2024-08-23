using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Hotelos;

[Dependency(ReplaceServices = true)]
public class HotelosBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Hotelos";
}
