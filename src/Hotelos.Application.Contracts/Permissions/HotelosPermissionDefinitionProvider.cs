using Hotelos.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Hotelos.Permissions;

public class HotelosPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HotelosPermissions.GroupName);

        var hotelGroup = context.AddGroup(HotelosPermissions.HotelGroupName);
        hotelGroup.AddPermission(HotelosPermissions.EditHotelProfile);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(HotelosPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HotelosResource>(name);
    }
}
