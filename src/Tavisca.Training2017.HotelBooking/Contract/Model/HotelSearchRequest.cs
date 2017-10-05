using System;

namespace Services.Model
{
    public class HotelSearchRequest
    {
        public string SearchText { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int PosId { get; set; }
    }
}
