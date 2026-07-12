using HotelRealQuito.Interfaces;
using HotelRealQuito.Models;

namespace HotelRealQuito.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly List<Reservation> _reservations = new();

        public void Save(Reservation reservation)
        {
            _reservations.Add(reservation);
        }

        public Reservation? GetById(string id)
        {
            return _reservations.FirstOrDefault(r => r.Id == id);
        }

        public List<Reservation> GetAll()
        {
            return new List<Reservation>(_reservations);
        }

        public void UpdateStatus(string id, ReservationStatus newStatus)
        {
            var reservation = GetById(id);
            if (reservation != null)
            {
                reservation.Status = newStatus;
            }
        }
    }
}
