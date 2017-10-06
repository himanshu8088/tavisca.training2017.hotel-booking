using BusinessLayer.Contracts;
using BusinessLayer.Model;
using Services.Contracts;
using System.Threading.Tasks;
using Services.Validator;
using System;
using System.Collections.Generic;

namespace Services
{
    public class HotelService : IHotelService
    {       
        public async Task<List<Services.Model.Hotel>> SearchHotelsAsync(Services.Model.HotelSearchRQ hotelSearchRQ)
        {
            HotelSearchRQValidator hotelSearchRQValidator = new HotelSearchRQValidator();
            
            if (hotelSearchRQValidator.IsValid(hotelSearchRQ) == false)
                throw new Exception("Invalid Search Request");

            IHotelSearch hotelSearch = BusinessLayer.Factories.Factory.Get<IHotelSearch>() as IHotelSearch;

            var hotelRQ = new BusinessLayer.Model.HotelSearchRQ()
            {
                SearchText = hotelSearchRQ.SearchText,
                CheckInDate = hotelSearchRQ.CheckInDate,
                CheckoutDate = hotelSearchRQ.CheckOutDate,
                SessionId = Guid.NewGuid()
            };

            Task<List<HotelItinerary>> hotelItineraries = hotelSearch.SearchAsync(hotelRQ);
            var itineraries = hotelItineraries.Result;            
            List<Services.Model.Hotel> hotels = new List<Model.Hotel>(); 

            foreach (var itinerary in itineraries)
            {
                var hotel = new Model.Hotel()
                {
                    Address = itinerary.Hotel.Address,
                    HotelName= itinerary.Hotel.HotelName,
                    StarRating = itinerary.Hotel.StarRating,
                    BaseFare = itinerary.BaseFare,
                    MediaUri=itinerary.MediaUri
                };                
                hotels.Add(hotel);
            }
            
            return hotels;
        }
    }
}

