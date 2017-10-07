using System;

namespace Services.Model
{
    public class HotelSearchRQ
    {
        public string SearchText { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public Location Location { get; set; }

        public int PsgCount { get; set; }

        public int NoOfRooms { get; set; }
    }
}
