using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Model
{
    public class RoomSearchRQ
    {
        public Guid SessionId { get; set; }

        public string SearchText { get; set; }

        public DateTime CheckinDate { get; set; }

        public DateTime CheckoutDate { get; set; }

        public Location Location { get; set; }

        public int PsgCount { get; set; }

        public int NoOfRooms { get; set; }

        public string HotelId { get; set; }
    }
}
