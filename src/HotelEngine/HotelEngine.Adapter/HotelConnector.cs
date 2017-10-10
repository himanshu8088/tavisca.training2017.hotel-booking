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
        public async Task<HotelEngine.Contracts.Models.HotelSearchRS> SearchHotelsAsync(HotelEngine.Contracts.Models.HotelSearchRQ hotelSearchRQ)
        {
            HotelEngineClient client = null;           
            StaticAdapterConfiguration config = new StaticAdapterConfiguration();
            var settings=config.GetHotelsAvailConfig(hotelSearchRQ);
            HotelEngine.Contracts.Models.HotelSearchRS hotelSearchRS = null;
            try
            {
                client = new HotelEngineClient();

                var hotelSearchReq = new Proxies.HotelSearchRQ()
                {
                    HotelSearchCriterion = settings.HotelSearchCriterion,
                    SessionId = hotelSearchRQ.SessionId.ToString(),
                    Filters = settings.Filters,
                    PagingInfo = settings.PagingInfo,
                    ResultRequested = settings.ResultRequested
                };

                var hotelSearchResponse = await client.HotelAvailAsync(hotelSearchReq);               
               
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
                            Description= roomProp.RoomDescription,
                            Id=roomProp.RoomId.ToString(),
                            Fare=new HotelEngine.Contracts.Models.Fare() { Amount=roomProp.DisplayRoomRate.BaseFare.Amount},
                            Name=roomProp.RoomName,
                            Type=roomProp.RoomType,
                            Bed=roomProp.BedType
                        };
                        rooms.Add(room);
                    }

                    List<Uri> urls = new List<Uri>();
                    foreach(var media in hotelProp.MediaContent)
                    {
                        var url = new Uri(media.Url);
                        urls.Add(url);
                    }

                    var hotel = new Hotel()
                    {
                        Address=hotelProp.Address.CompleteAddress,
                        Fare=new HotelEngine.Contracts.Models.Fare()
                        {
                            Amount = itinerary.Fare.BaseFare.Amount                                                        
                        },
                        HotelId= hotelProp.Id,
                        HotelName=hotelProp.Name,
                        Location=new HotelEngine.Contracts.Models.Location()
                        {
                            Latitude= hotelProp.Address.GeoCode.Latitude,
                            Longitude= hotelProp.Address.GeoCode.Latitude
                        }, 
                        Rooms=rooms,
                        StarRating= hotelProp.HotelRating.Rating,
                        Images=urls
                        
                    };
                    hotels.Add(hotel);
                }

                hotelSearchRS = new HotelEngine.Contracts.Models.HotelSearchRS()
                {
                    SessionId= hotelSearchRQ.SessionId.ToString(),
                    Hotels=hotels
                    
                };              
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                await client.CloseAsync();
            }
            return hotelSearchRS;
        }

        public Task<RoomSearchRS> SearchRoomsAsync(RoomSearchRQ roomSearchRQ)
        {
            throw new NotImplementedException();
        }
    }
}
