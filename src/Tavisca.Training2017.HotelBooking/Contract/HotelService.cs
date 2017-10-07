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
        public async Task<List<Services.Model.Hotel>> SearchHotelsAsync(Services.Model.HotelSearchRQ searchRQ)
        {
            HotelSearchRQValidator hotelSearchRQValidator = new HotelSearchRQValidator();
            
            if (hotelSearchRQValidator.IsValid(searchRQ) == false)
                throw new Exception("Invalid Search Request");

            IHotelSearch hotelSearch = BusinessLayer.Factories.Factory.Get<IHotelSearch>() as IHotelSearch;

            var hotelRQ = new BusinessLayer.Model.HotelSearchRQ()
            {
                SessionId = Guid.NewGuid(),
                SearchText = searchRQ.SearchText,
                CheckInDate = searchRQ.CheckInDate,
                CheckoutDate = searchRQ.CheckOutDate,
                Location = new BusinessLayer.Model.Location()
                {
                    Latitude = searchRQ.Location.Latitude,
                    Longitude = searchRQ.Location.Longitude
                },
                NoOfRooms = searchRQ.NoOfRooms,
                PsgCount = searchRQ.PsgCount
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
                    MediaUri=itinerary.MediaUri,
                    Location = new Services.Model.Location()
                    {
                        Latitude = itinerary.Location.Latitude,
                        Longitude = itinerary.Location.Longitude
                    }
                };                
                hotels.Add(hotel);
            }
            
            return hotels;
        }
    }
}

