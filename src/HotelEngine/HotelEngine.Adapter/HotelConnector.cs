using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using HotelSearch;
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
        private HotelEngineClient _client = null;

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
            var roomPriceSearchRS = ParsePriceRS(hotelRoomPriceRS);
            return roomPriceSearchRS;
        }

        private RoomPriceSearchRS ParsePriceRS(HotelRoomPriceRS hotelRoomPriceRS)
        {
            var roomPriceSearchRS = new RoomPriceSearchRS()
            {
                Fare = new HotelEngine.Contracts.Models.Fare()
                {
                    Amount = hotelRoomPriceRS.Itinerary.Fare.BaseFare.Amount
                },
                SessionId = hotelRoomPriceRS.SessionId
            };
            return roomPriceSearchRS;
        }

        public async Task<HotelRoomPriceRS> GetRoomPrice(HotelRoomPriceRQ hotelRoomPriceRQ)
        {
            HotelRoomPriceRS hotelRoomPriceRS = null;
            try
            {
                _client = new HotelEngineClient();
                hotelRoomPriceRS = await _client.HotelRoomPriceAsync(hotelRoomPriceRQ);
            }
            catch (Exception e)
            {
                throw new Exception("Connection Error");
            }
            finally
            {
                await _client.CloseAsync();
            }
            return hotelRoomPriceRS;
        }

        private async Task<HotelRoomAvailRS> GetRoomsAsync(HotelRoomAvailRQ hotelRoomAvailRQ)
        {
            HotelRoomAvailRS hotelRoomAvailRS;
            try
            {
                _client = new HotelEngineClient();
                hotelRoomAvailRS = await _client.HotelRoomAvailAsync(hotelRoomAvailRQ);
            }
            catch (Exception e)
            {
                throw new Exception("Connection Error");
            }
            finally
            {
                await _client.CloseAsync();
            }
            return hotelRoomAvailRS;
        }

        private async Task<HotelSearch.HotelSearchRS> GetHotelsAsync(HotelSearch.HotelSearchRQ request)
        {
            HotelSearch.HotelSearchRS hotelSearchRS;
            try
            {
                _client = new HotelEngineClient();
                hotelSearchRS = await _client.HotelAvailAsync(request);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                await _client.CloseAsync();
            }
            return hotelSearchRS;
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

        private HotelRoomAvailRQ ParseRoomRQ(RoomSearchRQ roomSearchRQ)
        {
            var roomsSettings = _config.GetRoomsAvailConfig(roomSearchRQ);
            var hotelRoomAvailRQ = new HotelRoomAvailRQ()
            {
                HotelSearchCriterion = roomsSettings.SearchCriterion,
                Itinerary = roomsSettings.HotelItinerary,
                ResultRequested = roomsSettings.ResultRequested,
                SessionId = roomSearchRQ.SessionId.ToString()
            };
            return hotelRoomAvailRQ;
        }

        private RoomSearchRS ParseRoomRS(HotelRoomAvailRS roomSearchResponse, Guid sessionId)
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

        private async Task<HotelRoomPriceRQ> ParsePriceRQ(RoomPriceSearchRQ roomPriceRQ)
        {
            var roomAvailRQ = ParseRoomRQ(roomPriceRQ);
            var roomAvailRS = await GetRoomsAsync(roomAvailRQ);
            var hotelRoomPriceRQ = new HotelRoomPriceRQ()
            {
                HotelSearchCriterion = roomAvailRQ.HotelSearchCriterion,
                Itinerary = roomAvailRS.Itinerary,
                SessionId = roomAvailRS.SessionId,
                ResultRequested = roomAvailRS.ResponseRecieved
            };
            return hotelRoomPriceRQ;
        }

        public Task<RoomBookRS> BookRoomAsync(RoomBookRQ roomBookRQ)
        {
            throw new NotImplementedException();
        }
    }
}
