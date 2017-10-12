using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using HotelSearch;
using HotelBook;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HotelEngine.Adapter.Configuration;
using HotelEngine.Adapter.Contracts;

namespace HotelEngine.Adapter
{
    public class HotelConnector : IHotelConnector
    {
        private IAdapterConfiguration _config;
        private HotelEngineClient _hotelEngineClient = null;
        private TripsEngineClient _tripEngineClient = null;

        public HotelConnector()
        {
            _config = new StaticAdapterConfiguration();
        }

        public async Task<HotelEngine.Contracts.Models.HotelSearchRS> SearchHotelsAsync(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ)
        {
            var hotelAvailRQ = ParseHotelRQ(hotelSearchRQ);
            var hotelAvailRS = await GetHotelsAsync(hotelAvailRQ);
            var hotelSearchRS = ParseHotelRS(hotelAvailRS, hotelSearchRQ.SessionId);
            return hotelSearchRS;
        }

        public async Task<RoomSearchRS> SearchRoomsAsync(RoomSearchRQ roomSearchRQ)
        {
            var roomAvailRQ = ParseRoomRQ(roomSearchRQ);
            var roomAvailRS = await GetRoomsAsync(roomAvailRQ);
            var roomSearchRS = ParseRoomRS(roomAvailRS, roomSearchRQ.SessionId);
            return roomSearchRS;
        }

        public async Task<RoomPriceSearchRS> SearchPriceAsync(RoomPriceSearchRQ roomPriceRQ)
        {
            var hotelRoomPriceRQ = await ParsePriceRQ(roomPriceRQ);
            var hotelRoomPriceRS = await GetRoomPrice(hotelRoomPriceRQ);
            var roomPriceSearchRS = ParsePriceRS(hotelRoomPriceRS, roomPriceRQ.SessionId);
            return roomPriceSearchRS;
        }

        private RoomPriceSearchRS ParsePriceRS(HotelSearch.HotelRoomPriceRS hotelRoomPriceRS, Guid sessionId)
        {
            var roomPriceSearchRS = new RoomPriceSearchRS()
            {
                Fare = new HotelEngine.Contracts.Models.Fare()
                {
                    Amount = hotelRoomPriceRS.Itinerary.Fare.BaseFare.Amount
                },
                SessionId = sessionId.ToString()
            };
            return roomPriceSearchRS;
        }

        private async Task<HotelSearch.HotelSearchRS> GetHotelsAsync(HotelSearch.HotelSearchRQ request)
        {
            HotelSearch.HotelSearchRS hotelSearchRS;
            try
            {
                _hotelEngineClient = new HotelEngineClient();
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

        private async Task<HotelSearch.HotelRoomAvailRS> GetRoomsAsync(HotelSearch.HotelRoomAvailRQ hotelRoomAvailRQ)
        {
            HotelSearch.HotelRoomAvailRS hotelRoomAvailRS;
            try
            {
                _hotelEngineClient = new HotelEngineClient();
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

        public async Task<HotelSearch.HotelRoomPriceRS> GetRoomPrice(HotelSearch.HotelRoomPriceRQ hotelRoomPriceRQ)
        {
            HotelSearch.HotelRoomPriceRS hotelRoomPriceRS = null;
            try
            {
                _hotelEngineClient = new HotelEngineClient();
                hotelRoomPriceRS = await _hotelEngineClient.HotelRoomPriceAsync(hotelRoomPriceRQ);
            }
            catch (Exception e)
            {
                throw new Exception("Connection Error");
            }
            finally
            {
                await _hotelEngineClient.CloseAsync();
            }
            return hotelRoomPriceRS;
        }

        public async Task<RoomBookRS> BookRoomAsync(RoomBookRQ roomBookRQ)
        {
            try
            {
                var roomBookRS = await _tripEngineClient.BookTripFolderAsync(new TripFolderBookRQ());
            }
            catch(Exception e)
            {
                throw new Exception("Connection Error");
            }
            finally
            {

            }
            throw new NotImplementedException();
        }        

        private HotelSearch.HotelSearchRQ ParseHotelRQ(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ)
        {
            var hotelSettings = _config.GetHotelsAvailConfig(hotelSearchRQ);
            var hotelSearchReq = new HotelSearch.HotelSearchRQ()
            {
                HotelSearchCriterion = hotelSettings.HotelSearchCriterion,
                SessionId = hotelSearchRQ.SessionId.ToString(),
                Filters = hotelSettings.Filters,
                PagingInfo = hotelSettings.PagingInfo,
                ResultRequested = hotelSettings.ResultRequested
            };
            return hotelSearchReq;
        }

        private HotelEngine.Contracts.Models.HotelSearchRS ParseHotelRS(HotelSearch.HotelSearchRS hotelSearchRS, Guid sessionId)
        {
            HotelEngine.Contracts.Models.HotelSearchRS hotelSearchResponse = null;
            var itineraries = hotelSearchRS.Itineraries;

            var hotels = new List<Hotel>();
            var rooms = new List<HotelEngine.Contracts.Models.Room>();

            foreach (var itinerary in itineraries)
            {
                var hotelProp = itinerary.HotelProperty;
                foreach (var roomProp in itinerary.Rooms)
                {
                    var room = new HotelEngine.Contracts.Models.Room()
                    {
                        Description = roomProp.RoomDescription,
                        Id = roomProp.RoomId.ToString(),
                        Fare = new HotelEngine.Contracts.Models.Fare() { Amount = roomProp.DisplayRoomRate.BaseFare.Amount },
                        Name = roomProp.RoomName,
                        Type = roomProp.RoomType,
                        Bed = roomProp.BedType
                    };
                    rooms.Add(room);
                }

                List<Uri> urls = new List<Uri>();
                foreach (var media in hotelProp.MediaContent)
                {
                    var url = new Uri(media.Url);
                    urls.Add(url);
                }

                List<HotelEngine.Contracts.Models.Amenity> amenities = new List<HotelEngine.Contracts.Models.Amenity>();
                foreach (var hotelAmenity in hotelProp.Amenities)
                {
                    var amenity = new HotelEngine.Contracts.Models.Amenity()
                    {
                        Name = hotelAmenity.Name,
                        Id = hotelAmenity.Id,
                    };
                    amenities.Add(amenity);
                }

                var hotel = new Hotel()
                {
                    Address = hotelProp.Address.CompleteAddress,
                    Fare = new HotelEngine.Contracts.Models.Fare()
                    {
                        Amount = itinerary.Fare.BaseFare.Amount
                    },
                    HotelId = hotelProp.Id,
                    HotelName = hotelProp.Name,
                    Location = new HotelEngine.Contracts.Models.Location()
                    {
                        Latitude = hotelProp.Address.GeoCode.Latitude,
                        Longitude = hotelProp.Address.GeoCode.Latitude
                    },
                    Rooms = rooms,
                    StarRating = hotelProp.HotelRating.Rating,
                    Images = urls,
                    Amenities = amenities
                };
                hotels.Add(hotel);
            }

            hotelSearchResponse = new HotelEngine.Contracts.Models.HotelSearchRS()
            {
                SessionId = sessionId.ToString(),
                Hotels = hotels
            };
            return hotelSearchResponse;
        }

        private HotelSearch.HotelRoomAvailRQ ParseRoomRQ(RoomSearchRQ roomSearchRQ)
        {
            var roomsSettings = _config.GetRoomsAvailConfig(roomSearchRQ);
            var hotelRoomAvailRQ = new HotelSearch.HotelRoomAvailRQ()
            {
                HotelSearchCriterion = roomsSettings.SearchCriterion,
                Itinerary = roomsSettings.HotelItinerary,
                ResultRequested = roomsSettings.ResultRequested,
                SessionId = roomSearchRQ.SessionId.ToString()
            };
            return hotelRoomAvailRQ;
        }

        private RoomSearchRS ParseRoomRS(HotelSearch.HotelRoomAvailRS roomSearchResponse, Guid sessionId)
        {
            RoomSearchRS roomSearchRS = null;
            List<HotelEngine.Contracts.Models.Room> rooms = new List<HotelEngine.Contracts.Models.Room>();

            foreach (var roomResult in roomSearchResponse.Itinerary.Rooms)
            {
                var room = new HotelEngine.Contracts.Models.Room()
                {
                    Bed = roomResult.BedType,
                    Description = roomResult.RoomDescription,
                    Fare = new HotelEngine.Contracts.Models.Fare() { Amount = roomResult.DisplayRoomRate.BaseFare.Amount },
                    Id = roomResult.RoomId.ToString(),
                    Name = roomResult.RoomName,
                    Type = roomResult.RoomType
                };
                rooms.Add(room);
            }

            roomSearchRS = new RoomSearchRS()
            {
                Rooms = rooms,
                SessionId = sessionId.ToString()/*roomSearchRQ.SessionId.ToString()*/
            };
            return roomSearchRS;
        }

        private async Task<HotelSearch.HotelRoomPriceRQ> ParsePriceRQ(RoomPriceSearchRQ roomPriceRQ)
        {
            var roomAvailRQ = ParseRoomRQ(roomPriceRQ);
            var roomAvailRS = await GetRoomsAsync(roomAvailRQ);
            var hotelRoomPriceRQ = new HotelSearch.HotelRoomPriceRQ()
            {
                HotelSearchCriterion = roomAvailRQ.HotelSearchCriterion,
                Itinerary = roomAvailRS.Itinerary,
                SessionId = roomAvailRS.SessionId,
                ResultRequested = roomAvailRS.ResponseRecieved
            };
            return hotelRoomPriceRQ;
        }

        
    }
}
