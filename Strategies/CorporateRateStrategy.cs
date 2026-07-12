using HotelRealQuito.Interfaces;
using HotelRealQuito.Models;

namespace HotelRealQuito.Strategies
{
    public class CorporateRateStrategy : IRateStrategy
    {
        public double CalculateCost(Reservation reservation)
        {
            return reservation.Nights * 60;
        }
    }
}
