using System;
using Proxies;
using HotelEngine.Adapter.Contracts;
using HotelEngine.Contracts.Models;
using Newtonsoft.Json;

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
        public TripProductConfig GetTripProductConfig(RoomPriceSearchRQ roomPriceSearchRQ, HotelRoomAvailRS hotelRoomAvailRS)
        {
            var roomsConfig=GetRoomsAvailConfig(roomPriceSearchRQ);
            var hotelItinerary = JsonConvert.DeserializeObject<BookingProxy.HotelItinerary>(JsonConvert.SerializeObject(hotelRoomAvailRS.Itinerary));
            var searchCriterion = JsonConvert.DeserializeObject<BookingProxy.HotelSearchCriterion>(JsonConvert.SerializeObject(roomsConfig.SearchCriterion));
            var tripProductConfig = new TripProductConfig(searchCriterion, hotelItinerary, roomPriceSearchRQ);
            return tripProductConfig;
        }
    }
}
