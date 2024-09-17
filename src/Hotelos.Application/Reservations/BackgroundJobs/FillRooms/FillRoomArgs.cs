namespace Hotelos.Application.Reservations.BackgroundJobs.FillRooms
{
    public class FillRoomArgs
    {
        public required int Id { get; set; }
        public required int ReservationId { get; set; }
    }
}
