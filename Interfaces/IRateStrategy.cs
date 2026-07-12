using HotelRealQuito.Models;

namespace HotelRealQuito.Interfaces
{
    public interface IRateStrategy
    {
        double CalculateCost(Reservation reservation);
    }
}
