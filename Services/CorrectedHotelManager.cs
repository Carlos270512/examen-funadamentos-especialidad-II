using HotelRealQuito.Interfaces;
using HotelRealQuito.Models;

namespace HotelRealQuito.Services
{
    public class CorrectedHotelManager
    {
        private readonly IReservationRepository _repository;
        private readonly List<IReservationObserver> _observers;

        public CorrectedHotelManager(IReservationRepository repository, List<IReservationObserver> observers)
        {
            _repository = repository;
            _observers = observers;
        }

        public void HandleReservation(string id, string guest, IRateStrategy rateStrategy, int nights)
        {
            var reservation = new Reservation
            {
                Id = id,
                GuestName = guest,
                Nights = nights,
                Status = ReservationStatus.Creada
            };

            reservation.TotalCost = rateStrategy.CalculateCost(reservation);

            _repository.Save(reservation);

            reservation.Status = ReservationStatus.Confirmada;
            _repository.UpdateStatus(reservation.Id, ReservationStatus.Confirmada);

            foreach (var observer in _observers)
            {
                observer.OnStatusChanged(reservation, ReservationStatus.Confirmada);
            }
        }
    }
}
