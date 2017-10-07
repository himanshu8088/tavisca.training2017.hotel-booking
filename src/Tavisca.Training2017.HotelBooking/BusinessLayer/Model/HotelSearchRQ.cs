using System;

namespace BusinessLayer.Model
{
    public class HotelSearchRQ
    {
        public Guid SessionId { get; set; }

        public string SearchText { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckoutDate { get; set; }
        
        public Location Location { get; set; }

        public int PsgCount { get; set; }

        public int NoOfRooms { get; set; }
    }
}
