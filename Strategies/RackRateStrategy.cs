using HotelRealQuito.Interfaces;
using HotelRealQuito.Models;

namespace HotelRealQuito.Strategies
{
    public class RackRateStrategy : IRateStrategy
    {
        public double CalculateCost(Reservation reservation)
        {
            return reservation.Nights * 80 * 1.2;
        }
    }
}
