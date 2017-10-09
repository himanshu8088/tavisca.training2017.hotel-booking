using System;

namespace Services.Model
{
    public class RoomSearchRQ
    {
        public string SearchText { get; set; }

        public DateTime CheckinDate { get; set; }

        public DateTime CheckoutDate { get; set; }

        public Location Location { get; set; }

        public int PsgCount { get; set; }

        public int NoOfRooms { get; set; }

        public string HotelId { get; set; }
    }
}
