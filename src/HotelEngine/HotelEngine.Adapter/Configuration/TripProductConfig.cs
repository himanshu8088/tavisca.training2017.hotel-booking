using System;
using System.Collections.Generic;
using System.Text;
using BookingProxy;
using Newtonsoft.Json;

namespace HotelEngine.Adapter.Configuration
{
    public class TripProductConfig
    {
        private HotelItinerary _hotelItinerary;
        private int _hotelId;
        private Guid _roomId;
        private HotelSearchCriterion _hotelSearchCriterion;

        public TripProductConfig(Proxies.HotelSearchCriterion searchCriterion, int hotelId, Guid roomId)
        {
            _hotelSearchCriterion = JsonConvert.DeserializeObject<BookingProxy.HotelSearchCriterion>(JsonConvert.SerializeObject(searchCriterion));
            _hotelId = hotelId;
            _roomId = roomId;
        }

        public BookingProxy.HotelSearchCriterion HotelSearchCriterion => _hotelSearchCriterion;
        public BookingProxy.HotelItinerary HotelItinerary => new HotelItinerary()
        {
            HotelProperty = new HotelProperty()
            {
                Id = _hotelId
            },
            Rooms = new BookingProxy.Room[]
            {
                new BookingProxy.Room()
                {
                    RoomId=_roomId
                }
            }
        };
            
    }
}
