using HotelRealQuito.Models;

namespace HotelRealQuito.Interfaces
{
    public interface IReservationObserver
    {
        void OnStatusChanged(Reservation reservation, ReservationStatus newStatus);
    }
}
