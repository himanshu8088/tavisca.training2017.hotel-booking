using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using Proxies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HotelEngine.Adapter.Configuration;

namespace HotelEngine.Adapter
{
    public class HotelConnector : IHotelConnector
    {
        private StaticAdapterConfiguration _config;
        private HotelEngineClient _client = null;

        public HotelConnector()
        {
            _config = new StaticAdapterConfiguration();
        }

        public async Task<HotelEngine.Contracts.Models.HotelSearchRS> SearchHotelsAsync(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ)
        {
            var hotelSettings = _config.GetHotelsAvailConfig(hotelSearchRQ);
            HotelEngine.Contracts.Models.HotelSearchRS hotelSearchRS = null;
            try
            {
                _client = new HotelEngineClient();

                var hotelSearchReq = new Proxies.HotelSearchRQ()
                {
                    HotelSearchCriterion = hotelSettings.HotelSearchCriterion,
                    SessionId = hotelSearchRQ.SessionId.ToString(),
                    Filters = hotelSettings.Filters,
                    PagingInfo = hotelSettings.PagingInfo,
                    ResultRequested = hotelSettings.ResultRequested
                };

                var hotelSearchResponse = await _client.HotelAvailAsync(hotelSearchReq);

                var itineraries = hotelSearchResponse.Itineraries;

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

                hotelSearchRS = new HotelEngine.Contracts.Models.HotelSearchRS()
                {
                    SessionId = hotelSearchRQ.SessionId.ToString(),
                    Hotels = hotels
                };
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

        public async Task<RoomSearchRS> SearchRoomsAsync(RoomSearchRQ roomSearchRQ)
        {
            var roomsSettings = _config.GetRoomsAvailConfig(roomSearchRQ);
            RoomSearchRS roomSearchRS = null;
            try
            {
                _client = new HotelEngineClient();

                var roomSearchReq = new HotelRoomAvailRQ()
                {
                    HotelSearchCriterion = roomsSettings.SearchCriterion,
                    Itinerary = roomsSettings.HotelItinerary,
                    ResultRequested = roomsSettings.ResultRequested,
                    SessionId = roomSearchRQ.SessionId.ToString()
                };

                var roomSearchResponse = await _client.HotelRoomAvailAsync(roomSearchReq);

                List<HotelEngine.Contracts.Models.Room> rooms = new List<HotelEngine.Contracts.Models.Room>();

                foreach (var roomResult in roomSearchResponse.Itinerary.Rooms)
                {
                    var room = new HotelEngine.Contracts.Models.Room()
                    {
                        Bed=roomResult.BedType,
                        Description= roomResult.RoomDescription,
                        Fare=new HotelEngine.Contracts.Models.Fare() { Amount=roomResult.DisplayRoomRate.BaseFare.Amount},
                        Id=roomResult.RoomId.ToString(),
                        Name=roomResult.RoomName,
                        Type=roomResult.RoomType
                    };
                    rooms.Add(room);
                }

                roomSearchRS = new RoomSearchRS()
                {
                    Rooms = rooms,
                    SessionId = roomSearchRQ.SessionId.ToString()
                };
                return roomSearchRS;

            }
            catch (Exception e)
            {

            }
            finally
            {
                await _client.CloseAsync();
            }
            return roomSearchRS;
        }
    }
}
