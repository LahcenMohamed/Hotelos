using Hotelos.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Hotelos.Permissions;

public class HotelosPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HotelosPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(HotelosPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HotelosResource>(name);
    }
}
