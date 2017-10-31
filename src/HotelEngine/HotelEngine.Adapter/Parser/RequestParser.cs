using BookingProxy;
using HotelEngine.Adapter.Contracts;
using HotelEngine.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelEngine.Adapter.Parser
{
    internal class RequestParser
    {
        private IRoviaProxyConfiguration _config;
        public RequestParser(IRoviaProxyConfiguration adapterConfiguration)
        {
            _config = adapterConfiguration;
        }

        internal Proxies.HotelSearchRQ ParseHotelSearchRQ(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ)
        {
            var hotelSettings = _config.GetMultiAvailConfig(hotelSearchRQ);
            var hotelSearchReq = new global::Proxies.HotelSearchRQ()
            {
                HotelSearchCriterion = hotelSettings.HotelSearchCriterion,
                SessionId = hotelSearchRQ.SessionId.ToString(),
                Filters = hotelSettings.Filters,
                PagingInfo = hotelSettings.PagingInfo,
                ResultRequested = hotelSettings.ResultRequested
            };
            return hotelSearchReq;
        }

        internal Proxies.HotelRoomAvailRQ ParseRoomSearchRQ(RoomSearchRQ roomSearchRQ)
        {
            var roomsSettings = _config.GetSingleAvailConfig(roomSearchRQ);
            var hotelRoomAvailRQ = new Proxies.HotelRoomAvailRQ()
            {
                HotelSearchCriterion = roomsSettings.SearchCriterion,
                Itinerary = roomsSettings.HotelItinerary,
                ResultRequested = roomsSettings.ResultRequested,
                SessionId = roomSearchRQ.SessionId.ToString()
            };
            return hotelRoomAvailRQ;
        }

        internal TripProductPriceRQ ParseRoomPriceSearchRQ(RoomPriceSearchRQ roomPriceSearchRQ, Proxies.HotelRoomAvailRS hotelRoomAvailRS)
        {            
            var settings = _config.GetTripProductConfig(roomPriceSearchRQ, hotelRoomAvailRS);

            TripProductPriceRQ tripProductPriceRQ = new TripProductPriceRQ()
            {
                ResultRequested = settings.ResultRequested,
                SessionId = roomPriceSearchRQ.SessionId.ToString(),
                TripProduct = new HotelTripProduct()
                {
                    HotelItinerary = settings.HotelItinerary,
                    HotelSearchCriterion = settings.HotelSearchCriterion
                }
            };

            return tripProductPriceRQ;
        }

        internal CompleteBookingRQ ParseCompleteBookingRQ(TripFolderBookRS tripFolderBookRS, Guid sessionId)
        {
            var settings = _config.GetCompleteBookConfig(tripFolderBookRS, sessionId);
            var rq = new CompleteBookingRQ()
            {
                TripFolderId = tripFolderBookRS.TripFolder.Id,
                SessionId = sessionId.ToString(),
                ExternalPayment = settings.ExternalPayment,
                ResultRequested = tripFolderBookRS.ResponseRecieved
            };
            return rq;
        }
    }
}
