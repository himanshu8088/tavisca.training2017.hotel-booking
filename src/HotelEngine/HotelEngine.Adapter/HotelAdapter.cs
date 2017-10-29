using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HotelEngine.Adapter.Configuration;
using HotelEngine.Adapter.Contracts;
using BookingProxy;
using Newtonsoft.Json;
using HotelEngine.Adapter.Parser;
using Proxies;
using HotelEngine.Adapter.Implementation;

namespace HotelEngine.Adapter
{
    public class HotelAdapter : IHotelAdapter
    {
        private IAdapterConfiguration _config;
        private HotelEngineClient _hotelEngineClient = null;
        private TripsEngineClient _tripEngineClient = null;        
        private RequestParser _requestParser;
        private ResponseParser _responseParser;

        public HotelAdapter()
        {
            _config = new StaticAdapterConfiguration();           
            _requestParser = new RequestParser(_config);
            _responseParser = new ResponseParser();
        }

        public async Task<HotelEngine.Contracts.Models.HotelSearchRS> SearchHotelsAsync(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ)
        {
            var multiAvailRQ = _requestParser.ParseHotelSearchRQ(hotelSearchRQ);
            var multiAvailRS = await GetHotelsAsync(multiAvailRQ);
            var hotelSearchRS = _responseParser.ParseHotelSearchRS(multiAvailRS, hotelSearchRQ.SessionId);
            return hotelSearchRS;
        }

        public async Task<RoomSearchRS> SearchRoomsAsync(RoomSearchRQ roomSearchRQ)
        {
            var singleAvailRQ = _requestParser.ParseRoomSearchRQ(roomSearchRQ);
            var singleAvailRS = await GetRoomsAsync(singleAvailRQ);
            var roomSearchRS = _responseParser.ParseRoomSearchRS(singleAvailRS, roomSearchRQ.SessionId);
            return roomSearchRS;
        }

        public async Task<RoomPriceSearchRS> SearchRoomPriceAsync(RoomPriceSearchRQ roomPriceSearchRQ)
        {            
            var singleAvailRQ = _requestParser.ParseRoomSearchRQ(roomPriceSearchRQ);
            var singleAvailRS = await GetRoomsAsync(singleAvailRQ);
            var tripProductPriceRQ = _requestParser.ParseRoomPriceSearchRQ(roomPriceSearchRQ, singleAvailRS);
            var tripProductPriceRS = await GetRoomPriceAsync(tripProductPriceRQ);
            var roomPriceSearchRS = _responseParser.ParseRoomPriceSearchRS(tripProductPriceRS);
            return roomPriceSearchRS;
        }

        public async Task<RoomBookRS> BookRoomAsync(RoomBookRQ roomBookRQ)
        {
            var tripFolderBookRS = await GetTripFolderBookAsync(roomBookRQ);
            var completeBookingRQ = _requestParser.ParseCompleteBookingRQ(tripFolderBookRS, roomBookRQ.SessionId);
            var completeBookingRS = await CompleteBookingAsync(completeBookingRQ);
            RoomBookRS roomBookRS = _responseParser.ParseRoomBookRS(completeBookingRS);
            return roomBookRS;
        }

        private async Task<Proxies.HotelSearchRS> GetHotelsAsync(Proxies.HotelSearchRQ request)
        {
            Proxies.HotelSearchRS hotelSearchRS;
            try
            {
                _hotelEngineClient = new Proxies.HotelEngineClient();
                hotelSearchRS = await _hotelEngineClient.HotelAvailAsync(request);
            }
            catch (Exception e)
            {
                throw new Exception("Connection Error");
            }
            finally
            {
                await _hotelEngineClient.CloseAsync();
            }
            return hotelSearchRS;
        }

        private async Task<Proxies.HotelRoomAvailRS> GetRoomsAsync(Proxies.HotelRoomAvailRQ hotelRoomAvailRQ)
        {
            Proxies.HotelRoomAvailRS hotelRoomAvailRS;
            try
            {
                _hotelEngineClient = new Proxies.HotelEngineClient();
                hotelRoomAvailRS = await _hotelEngineClient.HotelRoomAvailAsync(hotelRoomAvailRQ);
            }
            catch (Exception e)
            {
                throw new Exception("Connection Error");
            }
            finally
            {
                await _hotelEngineClient.CloseAsync();
            }
            return hotelRoomAvailRS;
        }

        private async Task<BookingProxy.TripProductPriceRS> GetRoomPriceAsync(TripProductPriceRQ tripProductPriceRQ)
        {
            TripProductPriceRS tripProductPriceRS = null;

            try
            {
                _tripEngineClient = new BookingProxy.TripsEngineClient();
                tripProductPriceRS = await _tripEngineClient.PriceTripProductAsync(tripProductPriceRQ);
            }
            catch (Exception e)
            {
                throw new Exception("Connection Error");
            }
            finally
            {
                await _hotelEngineClient.CloseAsync();
            }
            return tripProductPriceRS;
        }

        private async Task<TripFolderBookRS> CreateTripFolderBookAsync(HotelTripProduct hotelTripProduct, RoomBookRQ roomBookRQ)
        {
            var settings = _config.GetTripFolderBookConfig(hotelTripProduct, roomBookRQ);
            TripFolderBookRS tripFolderBookRS = null;
            try
            {
                _tripEngineClient = new TripsEngineClient();
                tripFolderBookRS = await _tripEngineClient.BookTripFolderAsync(settings.TripFolderBookRQ);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to Create Book Folder");
            }
            finally
            {
                await _tripEngineClient.CloseAsync();
            }
            return tripFolderBookRS;
        }

        private async Task<TripFolderBookRS> GetTripFolderBookAsync(RoomBookRQ roomBookRQ)
        {           
            var singleAvailRQ = _requestParser.ParseRoomSearchRQ(roomBookRQ);
            var singleAvailRS = await GetRoomsAsync(singleAvailRQ);
            var tripProductPriceRQ = _requestParser.ParseRoomPriceSearchRQ(roomBookRQ, singleAvailRS);
            var tripProductPriceRS = await GetRoomPriceAsync(tripProductPriceRQ);
            var tripProduct=(HotelTripProduct)tripProductPriceRS.TripProduct;
            var tripFolderBookRS = await CreateTripFolderBookAsync(tripProduct, roomBookRQ);
            return tripFolderBookRS;
        }

        private async Task<CompleteBookingRS> CompleteBookingAsync(CompleteBookingRQ completeBookingRQ)
        {
            CompleteBookingRS completeBookingRS;
            try
            {
                _tripEngineClient = new TripsEngineClient();
                completeBookingRS = await _tripEngineClient.CompleteBookingAsync(completeBookingRQ);
            }
            catch (Exception e)
            {
                throw new Exception("Connection Error");
            }
            finally
            {
                await _tripEngineClient.CloseAsync();
            }
            return completeBookingRS;
        }


      

        

    }
}
