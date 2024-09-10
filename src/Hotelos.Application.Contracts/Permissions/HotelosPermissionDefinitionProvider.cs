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

        var roomGroup = context.AddGroup(HotelosPermissions.RoomGroupName, L("Permission:Rooms:Name"));
        roomGroup.AddPermission(HotelosPermissions.CreateRoom, L("Permission:Rooms:Create"));
        roomGroup.AddPermission(HotelosPermissions.UpdateRoom, L("Permission:Rooms:Update"));
        roomGroup.AddPermission(HotelosPermissions.DeleteRoom, L("Permission:Rooms:Delete"));
        roomGroup.AddPermission(HotelosPermissions.GetRooms, L("Permission:Rooms:Get"));

        var clientGroup = context.AddGroup(HotelosPermissions.ClientGroupName, L("Permission:Clients:Name"));
        clientGroup.AddPermission(HotelosPermissions.CreateClient, L("Permission:Clients:Create"));
        clientGroup.AddPermission(HotelosPermissions.UpdateClient, L("Permission:Clients:Update"));
        clientGroup.AddPermission(HotelosPermissions.DeleteClient, L("Permission:Clients:Delete"));
        clientGroup.AddPermission(HotelosPermissions.GetClients, L("Permission:Clients:Get"));

        var serviceGroup = context.AddGroup(HotelosPermissions.ServiceGroupName, L("Permission:Services:Name"));
        serviceGroup.AddPermission(HotelosPermissions.CreateService, L("Permission:Services:Create"));
        serviceGroup.AddPermission(HotelosPermissions.UpdateService, L("Permission:Services:Update"));
        serviceGroup.AddPermission(HotelosPermissions.DeleteService, L("Permission:Services:Delete"));
        serviceGroup.AddPermission(HotelosPermissions.GetServices, L("Permission:Services:Get"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(HotelosPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HotelosResource>(name);
    }
}
