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

        var floorGroup = context.AddGroup(HotelosPermissions.FloorsGroupName, L("Permission:Floors:Name"));
        floorGroup.AddPermission(HotelosPermissions.CreateFloor, L("Permission:Floors:Create"));
        floorGroup.AddPermission(HotelosPermissions.UpdateFloor, L("Permission:Floors:Update"));
        floorGroup.AddPermission(HotelosPermissions.DeleteFloor, L("Permission:Floors:Delete"));
        floorGroup.AddPermission(HotelosPermissions.GetAllFloors, L("Permission:Floors:GetAll"));

        var roomTypeGroup = context.AddGroup(HotelosPermissions.RoomTypesGroupName, L("Permission:RoomTypes:Name"));
        roomTypeGroup.AddPermission(HotelosPermissions.CreateRoomType, L("Permission:RoomTypes:Create"));
        roomTypeGroup.AddPermission(HotelosPermissions.UpdateRoomType, L("Permission:RoomTypes:Update"));
        roomTypeGroup.AddPermission(HotelosPermissions.DeleteRoomType, L("Permission:RoomTypes:Delete"));
        roomTypeGroup.AddPermission(HotelosPermissions.GetAllRoomType, L("Permission:RoomTypes:GetAll"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(HotelosPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HotelosResource>(name);
    }
}
