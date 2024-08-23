using Volo.Abp.Settings;

namespace Hotelos.Settings;

public class HotelosSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(HotelosSettings.MySetting1));
    }
}
