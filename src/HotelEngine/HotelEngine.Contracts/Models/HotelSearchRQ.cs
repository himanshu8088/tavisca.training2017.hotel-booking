using System;

namespace HotelEngine.Contracts.Models
{
    public class HotelSearchRQ
    {
        public Guid SessionId { get; set; }

        public string SearchText { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }
        
        public GeoCode GeoCode { get; set; }

        public int GuestCount { get; set; }

        public int NoOfRooms { get; set; }
    }
}
