using Hotelos.Domain.Reservations;
using Hotelos.Domain.Rooms;
using Hotelos.Domain.Shared.Reservations.Enums;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Hotelos.Application.Reservations.BackgroundJobs.FillRooms
{
    public class FillRoom : AsyncBackgroundJob<FillRoomArgs>, ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<Reservation> _reservationRepository;

        public FillRoom(
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<Room> roomRepository,
            IRepository<Reservation> reservationRepository)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
        }

        [UnitOfWork]
        public override async Task ExecuteAsync(FillRoomArgs args)
        {
            using var uow = _unitOfWorkManager.Begin(requiresNew: true);

            var check = await _reservationRepository.AnyAsync(x => x.Id == args.ReservationId &&
                                                              x.Type == ReservationType.Confirmed);
            if (check)
            {
                var room = await _roomRepository.FirstOrDefaultAsync(x => x.Id == args.Id);
                if (room != null)
                {
                    room.UpdateVacant(true);
                    await _roomRepository.UpdateAsync(room);
                }
            }

            await uow.CompleteAsync();
        }
    }
}
