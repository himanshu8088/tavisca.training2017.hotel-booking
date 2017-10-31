using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using System;
using System.Threading.Tasks;
using HotelEngine.Adapter.Contracts;
using BookingProxy;
using HotelEngine.Adapter.Parser;
using Proxies;
using HotelEngine.Adapter.Implementation;

namespace HotelEngine.Adapter
{
    public class HotelAdapter : IHotelAdapter
    {
        private IRoviaProxyConfiguration _config;
        private RequestParser _requestParser;
        private ResponseParser _responseParser;
        private Engines.HotelEngine _hotelEngine;
        private Engines.BookingEngine _bookingEngine;

        public HotelAdapter()
        {
            _config = new StaticProxyConfiguration();           
            _requestParser = new RequestParser(_config);
            _responseParser = new ResponseParser();
            _hotelEngine = new Engines.HotelEngine();
            _bookingEngine = new Engines.BookingEngine();
        }

        public async Task<HotelEngine.Contracts.Models.HotelSearchRS> SearchHotelsAsync(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ)
        {
            var multiAvailRQ = _requestParser.ParseHotelSearchRQ(hotelSearchRQ);
            var multiAvailRS = await _hotelEngine.GetHotelsAsync(multiAvailRQ);
            var hotelSearchRS = _responseParser.ParseHotelSearchRS(multiAvailRS, hotelSearchRQ.SessionId);
            return hotelSearchRS;
        }

        public async Task<RoomSearchRS> SearchRoomsAsync(RoomSearchRQ roomSearchRQ)
        {
            var singleAvailRQ = _requestParser.ParseRoomSearchRQ(roomSearchRQ);
            var singleAvailRS = await _hotelEngine.GetRoomsAsync(singleAvailRQ);
            var roomSearchRS = _responseParser.ParseRoomSearchRS(singleAvailRS, roomSearchRQ.SessionId);
            return roomSearchRS;
        }

        public async Task<RoomPriceSearchRS> SearchRoomPriceAsync(RoomPriceSearchRQ roomPriceSearchRQ)
        {            
            var singleAvailRQ = _requestParser.ParseRoomSearchRQ(roomPriceSearchRQ);
            var singleAvailRS = await _hotelEngine.GetRoomsAsync(singleAvailRQ);
            var tripProductPriceRQ = _requestParser.ParseRoomPriceSearchRQ(roomPriceSearchRQ, singleAvailRS);
            var tripProductPriceRS = await _bookingEngine.GetRoomPriceAsync(tripProductPriceRQ);
            var roomPriceSearchRS = _responseParser.ParseRoomPriceSearchRS(tripProductPriceRS);
            return roomPriceSearchRS;
        }

        public async Task<RoomBookRS> BookRoomAsync(RoomBookRQ roomBookRQ)
        {
            var tripFolderBookRS = await GetTripFolderBookAsync(roomBookRQ);
            var completeBookingRQ = _requestParser.ParseCompleteBookingRQ(tripFolderBookRS, roomBookRQ.SessionId);
            var completeBookingRS = await _bookingEngine.CompleteBookingAsync(completeBookingRQ);
            RoomBookRS roomBookRS = _responseParser.ParseRoomBookRS(completeBookingRS);
            return roomBookRS;
        }

        private async Task<TripFolderBookRS> GetTripFolderBookAsync(RoomBookRQ roomBookRQ)
        {
            var singleAvailRQ = _requestParser.ParseRoomSearchRQ(roomBookRQ);
            var singleAvailRS = await _hotelEngine.GetRoomsAsync(singleAvailRQ);
            var tripProductPriceRQ = _requestParser.ParseRoomPriceSearchRQ(roomBookRQ, singleAvailRS);
            var tripProductPriceRS = await _bookingEngine.GetRoomPriceAsync(tripProductPriceRQ);
            var tripProduct = (HotelTripProduct)tripProductPriceRS.TripProduct;
            var settings = _config.GetTripFolderBookConfig(tripProduct, roomBookRQ);
            var tripFolderBookRS = await _bookingEngine.CreateTripFolderBookAsync(settings.TripFolderBookRQ);
            return tripFolderBookRS;
        }

    }
}
