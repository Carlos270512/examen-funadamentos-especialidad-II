using HotelRealQuito.Models;

namespace HotelRealQuito.Interfaces
{
    public interface IReservationRepository
    {
        void Save(Reservation reservation);
        Reservation? GetById(string id);
        List<Reservation> GetAll();
        void UpdateStatus(string id, ReservationStatus newStatus);
    }
}
