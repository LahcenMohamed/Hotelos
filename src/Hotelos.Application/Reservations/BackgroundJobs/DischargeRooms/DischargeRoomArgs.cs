namespace Hotelos.Application.Reservations.BackgroundJobs.DischargeRooms
{
    public sealed class DischargeRoomArgs
    {
        public required int Id { get; set; }
        public required int ReservationId { get; set; }
    }
}
