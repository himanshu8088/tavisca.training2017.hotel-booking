using System;
using Proxies;
using HotelEngine.Adapter.Contracts;
using HotelEngine.Contracts.Models;

namespace HotelEngine.Adapter.Configuration
{

    public class StaticAdapterConfiguration : IAdapterConfiguration
    {
        public HotelsAvailConfig GetHotelsAvailConfig(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ)
        {
            return new HotelsAvailConfig(hotelSearchRQ);
        }
        public RoomsAvailConfig GetRoomsAvailConfig(RoomSearchRQ roomSearchRQ)
        {
            var hotelConfig = new HotelsAvailConfig(roomSearchRQ);
            return new RoomsAvailConfig(hotelConfig, roomSearchRQ);
        }
        public TripProductConfig GetTripProductConfig(RoomPriceSearchRQ hotelRoomPriceRQ)
        {
            var roomsConfig=GetRoomsAvailConfig(hotelRoomPriceRQ);
            var tripProductConfig = new TripProductConfig(roomsConfig.SearchCriterion, hotelRoomPriceRQ.HotelId, hotelRoomPriceRQ.RoomId);
            return tripProductConfig;
        }
    }
}
