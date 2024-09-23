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

        var reservationGroup = context.AddGroup(HotelosPermissions.ReservationGroupName, L("Permission:Reservations:Name"));
        reservationGroup.AddPermission(HotelosPermissions.CreateReservation, L("Permission:Reservations:Create"));
        reservationGroup.AddPermission(HotelosPermissions.UpdateReservation, L("Permission:Reservations:Update"));
        reservationGroup.AddPermission(HotelosPermissions.DeleteReservation, L("Permission:Reservations:Delete"));
        reservationGroup.AddPermission(HotelosPermissions.GetReservations, L("Permission:Reservations:Get"));

        var jobTypeGroup = context.AddGroup(HotelosPermissions.JobTypeGroupName, L("Permission:JobTypes:Name"));
        jobTypeGroup.AddPermission(HotelosPermissions.CreateJobType, L("Permission:JobTypes:Create"));
        jobTypeGroup.AddPermission(HotelosPermissions.UpdateJobType, L("Permission:JobTypes:Update"));
        jobTypeGroup.AddPermission(HotelosPermissions.DeleteJobType, L("Permission:JobTypes:Delete"));
        jobTypeGroup.AddPermission(HotelosPermissions.GetJobTypes, L("Permission:JobTypes:Get"));

        var employeeGroup = context.AddGroup(HotelosPermissions.EmployeeGroupName, L("Permission:Employees:Name"));
        employeeGroup.AddPermission(HotelosPermissions.CreateEmployee, L("Permission:Employees:Create"));
        employeeGroup.AddPermission(HotelosPermissions.UpdateEmployee, L("Permission:Employees:Update"));
        employeeGroup.AddPermission(HotelosPermissions.DeleteEmployee, L("Permission:Employees:Delete"));
        employeeGroup.AddPermission(HotelosPermissions.GetEmployees, L("Permission:Employees:Get"));

        var jobTimeGroup = context.AddGroup(HotelosPermissions.JobTimeGroupName, L("Permission:JobTimes:Name"));
        jobTimeGroup.AddPermission(HotelosPermissions.CreateJobTime, L("Permission:JobTimes:Create"));
        jobTimeGroup.AddPermission(HotelosPermissions.UpdateJobTime, L("Permission:JobTimes:Update"));
        jobTimeGroup.AddPermission(HotelosPermissions.DeleteJobTime, L("Permission:JobTimes:Delete"));
        jobTimeGroup.AddPermission(HotelosPermissions.GetJobTimes, L("Permission:JobTimes:Get"));

        var subscriptionGroup = context.AddGroup(HotelosPermissions.SubscriptionGroupName, L("Permission:Subscriptions:Name"));
        subscriptionGroup.AddPermission(HotelosPermissions.CreateSubscription, L("Permission:Subscriptions:Create"));
        subscriptionGroup.AddPermission(HotelosPermissions.UpdateSubscription, L("Permission:Subscriptions:Update"));
        subscriptionGroup.AddPermission(HotelosPermissions.DeleteSubscription, L("Permission:Subscriptions:Delete"));
        subscriptionGroup.AddPermission(HotelosPermissions.GetSubscriptions, L("Permission:Subscriptions:Get"));

        var subscriptionHotelGroup = context.AddGroup(HotelosPermissions.SubscriptionHotelGroupName, L("Permission:SubscriptionHotels:Name"));
        subscriptionHotelGroup.AddPermission(HotelosPermissions.CreateSubscriptionHotel, L("Permission:SubscriptionHotels:Create"));
        subscriptionHotelGroup.AddPermission(HotelosPermissions.UpdateSubscriptionHotel, L("Permission:SubscriptionHotels:Update"));
        subscriptionHotelGroup.AddPermission(HotelosPermissions.DeleteSubscriptionHotel, L("Permission:SubscriptionHotels:Delete"));
        subscriptionHotelGroup.AddPermission(HotelosPermissions.GetSubscriptionHotels, L("Permission:SubscriptionHotels:Get"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(HotelosPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HotelosResource>(name);
    }
}
