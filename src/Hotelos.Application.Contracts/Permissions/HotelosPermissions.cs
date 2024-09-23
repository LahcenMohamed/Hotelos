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

    public const string ClientGroupName = GroupName + ".Client";
    public const string CreateClient = ClientGroupName + "Create";
    public const string UpdateClient = ClientGroupName + "Update";
    public const string DeleteClient = ClientGroupName + "Delete";
    public const string GetClients = ClientGroupName + "Get";

    public const string ServiceGroupName = GroupName + ".Service";
    public const string CreateService = ServiceGroupName + "Create";
    public const string UpdateService = ServiceGroupName + "Update";
    public const string DeleteService = ServiceGroupName + "Delete";
    public const string GetServices = ServiceGroupName + "Get";

    public const string ReservationGroupName = GroupName + ".Reservation";
    public const string CreateReservation = ReservationGroupName + "Create";
    public const string UpdateReservation = ReservationGroupName + "Update";
    public const string DeleteReservation = ReservationGroupName + "Delete";
    public const string GetReservations = ReservationGroupName + "Get";

    public const string JobTypeGroupName = GroupName + ".JobType";
    public const string CreateJobType = JobTypeGroupName + "Create";
    public const string UpdateJobType = JobTypeGroupName + "Update";
    public const string DeleteJobType = JobTypeGroupName + "Delete";
    public const string GetJobTypes = JobTypeGroupName + "Get";

    public const string EmployeeGroupName = GroupName + ".Employee";
    public const string CreateEmployee = EmployeeGroupName + "Create";
    public const string UpdateEmployee = EmployeeGroupName + "Update";
    public const string DeleteEmployee = EmployeeGroupName + "Delete";
    public const string GetEmployees = EmployeeGroupName + "Get";

    public const string JobTimeGroupName = GroupName + ".JobTime";
    public const string CreateJobTime = JobTimeGroupName + "Create";
    public const string UpdateJobTime = JobTimeGroupName + "Update";
    public const string DeleteJobTime = JobTimeGroupName + "Delete";
    public const string GetJobTimes = JobTimeGroupName + "Get";

    public const string SubscriptionGroupName = GroupName + ".Subscription";
    public const string CreateSubscription = SubscriptionGroupName + "Create";
    public const string UpdateSubscription = SubscriptionGroupName + "Update";
    public const string DeleteSubscription = SubscriptionGroupName + "Delete";
    public const string GetSubscriptions = SubscriptionGroupName + "Get";

    public const string SubscriptionHotelGroupName = GroupName + ".SubscriptionHotel";
    public const string CreateSubscriptionHotel = SubscriptionHotelGroupName + "Create";
    public const string UpdateSubscriptionHotel = SubscriptionHotelGroupName + "Update";
    public const string DeleteSubscriptionHotel = SubscriptionHotelGroupName + "Delete";
    public const string GetSubscriptionHotels = SubscriptionHotelGroupName + "Get";
    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
