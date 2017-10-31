using System;
using Proxies;
using HotelEngine.Adapter.Contracts;
using HotelEngine.Contracts.Models;
using Newtonsoft.Json;
using BookingProxy;
using HotelEngine.Adapter.Configuration;

namespace HotelEngine.Adapter.Implementation
{
    internal class StaticProxyConfiguration : IRoviaProxyConfiguration
    {
        public HotelsAvailConfig GetMultiAvailConfig(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ)
        {
            return new HotelsAvailConfig(hotelSearchRQ);
        }
        public RoomsAvailConfig GetSingleAvailConfig(RoomSearchRQ roomSearchRQ)
        {
            var hotelConfig = new HotelsAvailConfig(roomSearchRQ);
            return new RoomsAvailConfig(hotelConfig, roomSearchRQ);
        }
        public TripProductConfig GetTripProductConfig(RoomPriceSearchRQ roomPriceSearchRQ, Proxies.HotelRoomAvailRS hotelRoomAvailRS)
        {
            var roomsConfig = GetSingleAvailConfig(roomPriceSearchRQ);
            var hotelItinerary = JsonConvert.DeserializeObject<BookingProxy.HotelItinerary>(JsonConvert.SerializeObject(hotelRoomAvailRS.Itinerary));
            var searchCriterion = JsonConvert.DeserializeObject<BookingProxy.HotelSearchCriterion>(JsonConvert.SerializeObject(roomsConfig.SearchCriterion));
            var tripProductConfig = new TripProductConfig(searchCriterion, hotelItinerary, roomPriceSearchRQ);
            return tripProductConfig;
        }

        public TripFolderBookConfig GetTripFolderBookConfig(HotelTripProduct hotelTripProduct, RoomBookRQ roomBookRQ)
        {            
            return new TripFolderBookConfig(hotelTripProduct, roomBookRQ);
        }

        public CompleteBookConfig GetCompleteBookConfig(TripFolderBookRS tripFolderBookRS, Guid sessionId)
        {
            return new CompleteBookConfig(tripFolderBookRS, sessionId);
        }
    }
}
