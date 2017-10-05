using BusinessLayer.Contracts;
using Connector.Contracts;
using Services.Contracts;
using Services.Model;
using System.Threading.Tasks;
using Factories =Tavisca.Training2017.HotelBooking.Factories;
using Services.Validator;
using System;
using System.Collections.Generic;
using Connector;

namespace Services
{
    public class HotelService : IHotelService
    {
        public async Task<HotelSearchResult> SearchHotelsAsync(HotelSearchRequest hotelSearchRQ)
        {
            HotelSearchRequestValidator hotelSearchRequestValidator = new HotelSearchRequestValidator();
            if (hotelSearchRequestValidator.IsValid(hotelSearchRQ) == false)
                throw new Exception("Invalid Search Request");

            IHotelConnector hotelConnector = Factories.Factory.Get<IHotelConnector>() as IHotelConnector;

            var hotelSearchRequest = new Tavisca.Training2017.HotelBooking.BusinessLayer.HotelSearchRequest()
            {
                SearchText = hotelSearchRQ.SearchText,
                CheckInDate = hotelSearchRQ.CheckInDate,
                CheckoutDate = hotelSearchRQ.CheckOutDate
            };

            IConfigurationService configurationService  = Factories.Factory.Get<IConfigurationService>() as IConfigurationService;
            hotelSearchRequest.PointOfSale = configurationService.GetPointofSale(hotelSearchRQ.PosId);
            
            List<HotelItinerary> hotelItineraries = await hotelConnector.SearchHotelsAsync(hotelSearchRequest);

            HotelSearchResult hotelSearchResult = new HotelSearchResult();
            hotelSearchResult.Hotels = new List<Hotel>();
            foreach (var itenary in hotelItineraries)
            {
                //hotelSearchResult.Hotels.Add(new Hotel()
                //{
                //    Name = itenary.
                //});
            }

            return hotelSearchResult;
        }
    }
}
