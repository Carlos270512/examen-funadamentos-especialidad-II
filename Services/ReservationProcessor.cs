using HotelRealQuito.Factories;
using HotelRealQuito.Interfaces;
using HotelRealQuito.Models;

namespace HotelRealQuito.Services
{
    public class ReservationProcessor
    {
        private readonly IReservationRepository _repository;
        private readonly List<IReservationObserver> _observers;

        public ReservationProcessor(IReservationRepository repository, List<IReservationObserver> observers)
        {
            _repository = repository;
            _observers = observers;
        }

        public void ProcessReservation(string id, string guestName, string roomType, int nights, string rateType)
        {
            var (reservation, strategy) = ReservationFactory.CreateReservation(
                id, guestName, roomType, nights, rateType);

            Console.WriteLine($"Reserva creada: ID={reservation.Id}, Guest={reservation.GuestName}, " +
                              $"RateType={reservation.RateType}, Nights={reservation.Nights}");

            reservation.TotalCost = strategy.CalculateCost(reservation);
            Console.WriteLine($"Costo calculado: ${reservation.TotalCost:F2} (usando {strategy.GetType().Name})");

            _repository.Save(reservation);
            Console.WriteLine($"Reserva guardada en repositorio. Estado actual: {reservation.Status}");


            reservation.Status = ReservationStatus.Confirmada;
            _repository.UpdateStatus(reservation.Id, ReservationStatus.Confirmada);

            NotifyObservers(reservation, ReservationStatus.Confirmada);

            Console.WriteLine($"\n>>> Reserva {reservation.Id} confirmada exitosamente. <<<\n");
        }

        private void NotifyObservers(Reservation reservation, ReservationStatus newStatus)
        {
            foreach (var observer in _observers)
            {
                observer.OnStatusChanged(reservation, newStatus);
            }
        }
    }
}
