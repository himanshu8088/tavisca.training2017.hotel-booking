
using System;

namespace BusinessLayer.Model
{
    public class HotelSearchRQ
    {
        public string SearchText { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckoutDate { get; set; }

        public Guid SessionId { get; set; }
    }
}
