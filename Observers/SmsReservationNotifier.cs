using HotelRealQuito.Interfaces;
using HotelRealQuito.Models;

namespace HotelRealQuito.Observers
{
    public class SmsReservationNotifier : IReservationObserver
    {
        public void OnStatusChanged(Reservation reservation, ReservationStatus newStatus)
        {
            Console.WriteLine($"SMS enviado a {reservation.GuestName} — " +
                              $"Reserva {reservation.Id} ahora está en estado: {newStatus}");
        }
    }
}
