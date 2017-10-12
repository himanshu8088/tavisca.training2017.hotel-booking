using System;
using System.Collections.Generic;
using System.Text;
using Proxies;
using HotelEngine.Contracts.Models;

namespace HotelEngine.Adapter.Configuration
{
    public class RoomsAvailConfig
    {
        private int _hotelId;
        private HotelSearchCriterion _hotelSearchCriterion;

        public RoomsAvailConfig(HotelsAvailConfig hotelAvailConfig, RoomSearchRQ roomSearchRQ)
        {
            _hotelId = roomSearchRQ.HotelId;
            _hotelSearchCriterion = hotelAvailConfig.HotelSearchCriterion;
        }

        public ResponseType ResultRequested => ResponseType.Unknown;

        public HotelItinerary HotelItinerary => new HotelItinerary()
        {
            HotelProperty = new HotelProperty()
            {
                Id = _hotelId
            }
        };

        public HotelSearchCriterion SearchCriterion => _hotelSearchCriterion;
    }
}
