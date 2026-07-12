using HotelRealQuito.Interfaces;
using HotelRealQuito.Models;

namespace HotelRealQuito.Strategies
{
    public class LowSeasonRateStrategy : IRateStrategy
    {
        public double CalculateCost(Reservation reservation)
        {
            return reservation.Nights * 80 * 0.85;
        }
    }
}
