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
        //public TripPriceConfig GetTripPriceConfig(RoomSearchRQ roomSearchRQ)
        //{
        //    var hotelConfig = new HotelsAvailConfig(roomSearchRQ);
        //    var roomConfig= new RoomsAvailConfig(hotelConfig, roomSearchRQ);
        //    return new TripPriceConfig(roomConfig);
        //}
    }
}
