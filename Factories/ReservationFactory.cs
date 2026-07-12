using HotelRealQuito.Interfaces;
using HotelRealQuito.Models;
using HotelRealQuito.Strategies;

namespace HotelRealQuito.Factories
{
    public class ReservationFactory
    {
        public static (Reservation reservation, IRateStrategy strategy) CreateReservation(
            string id, string guestName, string roomType, int nights, string rateType)
        {
            var reservation = new Reservation
            {
                Id = id,
                GuestName = guestName,
                RoomType = roomType,
                Nights = nights,
                RateType = rateType,
                Status = ReservationStatus.Creada,
                TotalCost = 0
            };

            IRateStrategy strategy = rateType switch
            {
                "temporadaAlta" => new RackRateStrategy(),
                "temporadaBaja" => new LowSeasonRateStrategy(),
                "corporativa" => new CorporateRateStrategy(),
                _ => throw new ArgumentException($"Tipo de tarifa '{rateType}' no válido. Valores permitidos: temporadaAlta, temporadaBaja, corporativa.")
            };

            return (reservation, strategy);
        }
    }
}
