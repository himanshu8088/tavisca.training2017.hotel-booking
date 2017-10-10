using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using HotelEngine.Contracts.Models;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Services.Validator;
using HotelEngine.Core.Implementation;

namespace HotelEngine.Services
{


    public class HotelService : IHotelService
    {
        private IHotelSearch _hotelSearch;

        public HotelService()
        {
            _hotelSearch = new HotelSearch();
        }
        
        public async Task<Hotel> RoomSearchAsync(RoomSearchRQ roomSearchRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<HotelSearchRS> SearchHotelsAsync(HotelSearchRQ hotelSearchRequest)
        {
            hotelSearchRequest.SessionId = Guid.NewGuid();
            var hotelSearchRS = await _hotelSearch.SearchAsync(hotelSearchRequest);           
            return hotelSearchRS;
        }



        //public async Task<HotelSearchRS> SearchHotelsAsync(HotelSearchRQ searchRQ)
        //{
        //    HotelSearchRQValidator hotelSearchRQValidator = new HotelSearchRQValidator();

        //    if (hotelSearchRQValidator.IsValid(searchRQ) == false)
        //        throw new Exception("Invalid Search Request");


        //    var hotelSearchRS = await _hotelSearch.SearchAsync(searchRQ);

        //IHotelSearch hotelSearch = Factories.Factory.Get<IHotelSearch>() as IHotelSearch;
        //var hotels = new List<Hotel>();
        //foreach (var resHotel in response.Hotels)
        //{
        //    var hotel = new Hotel()
        //    {
        //        HotelId = resHotel.HotelId,
        //        Address = itinerary.Hotel.Address,
        //        HotelName= itinerary.Hotel.HotelName,
        //        StarRating = itinerary.Hotel.StarRating,
        //        BaseFare = itinerary.BaseFare,
        //        Images=itinerary.MediaUri,
        //        Location = new Location()
        //        {
        //            Latitude = itinerary.Location.Latitude,
        //            Longitude = itinerary.Location.Longitude
        //        }
        //    };                
        //    hotels.Add(hotel);
        //}
        //    return hotelSearchRS;
        //}

        //public async Task<Hotel> RoomSearchAsync(RoomSearchRQ roomSearchRequest)
        //{
        //    var roomSearch = Factories.Factory.Get<IRoomSearch>() as IRoomSearch;
        //    var hotelItinerary = await roomSearch.SearchAsync(roomSearchRequest);
        //    var hotel = new Hotel()
        //    {
        //        HotelId = hotelItinerary.Hotel.HotelId,
        //        Address = hotelItinerary.Hotel.Address,
        //        HotelName = hotelItinerary.Hotel.HotelName,
        //        StarRating = hotelItinerary.Hotel.StarRating,
        //        BaseFare = hotelItinerary.BaseFare,
        //        Images = hotelItinerary.MediaUri,
        //        Location = new Location()
        //        {
        //            Latitude = hotelItinerary.Location.Latitude,
        //            Longitude = hotelItinerary.Location.Longitude
        //        },
        //    };
        //    return hotel;
        //}
    }
}

