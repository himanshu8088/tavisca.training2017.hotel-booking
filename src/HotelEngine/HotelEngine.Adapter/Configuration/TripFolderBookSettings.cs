using BookingProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelEngine.Adapter.Configuration
{
    public class TripFolderBookSettings
    {
        public string TripFolderName { get; set; }
        public int Age { get; set; }
        public DateTime Birthdate { get; set; }
        public Money Amount { get; set; }
        public string SessionId { get; set; }
        public HotelItinerary HotelItinerary { get; set; }
        public HotelSearchCriterion HotelSearchCriterion { get; set; }
        public int[] Ages { get; set; }
        public int Qty { get; set; }
    }
}
