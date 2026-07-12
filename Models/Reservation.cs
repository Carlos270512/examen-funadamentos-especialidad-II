namespace HotelRealQuito.Models
{
    public class Reservation
    {
        public string Id { get; set; } = string.Empty;
        public string GuestName { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty;
        public int Nights { get; set; }
        public string RateType { get; set; } = string.Empty; // "temporadaAlta", "temporadaBaja", "corporativa"
        public ReservationStatus Status { get; set; }
        public double TotalCost { get; set; }
    }
}
