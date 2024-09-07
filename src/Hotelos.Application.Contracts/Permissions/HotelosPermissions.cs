namespace Hotelos.Permissions;

public static class HotelosPermissions
{
    public const string GroupName = "Hotelos";

    public const string HotelGroupName = GroupName + ".Hotels";
    public const string EditHotelProfile = GroupName + ".EditHotelProfile";

    public const string FloorsGroupName = GroupName + ".Floors";
    public const string CreateFloor = FloorsGroupName + "Create";
    public const string UpdateFloor = FloorsGroupName + "Update";
    public const string DeleteFloor = FloorsGroupName + "Delete";
    public const string GetAllFloors = FloorsGroupName + "GetAll";

    public const string RoomTypesGroupName = GroupName + ".RoomTypes";
    public const string CreateRoomType = RoomTypesGroupName + "Create";
    public const string UpdateRoomType = RoomTypesGroupName + "Update";
    public const string DeleteRoomType = RoomTypesGroupName + "Delete";
    public const string GetAllRoomType = RoomTypesGroupName + "GetAll";

    public const string RoomGroupName = GroupName + ".Room";
    public const string CreateRoom = RoomGroupName + "Create";
    public const string UpdateRoom = RoomGroupName + "Update";
    public const string DeleteRoom = RoomGroupName + "Delete";
    public const string GetRooms = RoomGroupName + "Get";
    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
