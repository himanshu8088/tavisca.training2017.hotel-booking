using BusinessLayer.Contracts;
using Connector.Contracts;
using System.Threading.Tasks;
using BusinessLayer.Factories;
using System;
using System.Collections.Generic;

namespace BusinessLayer.ContractsImplementation
{
    public class HotelSearch : IHotelSearch
    {
        public async Task<List<BusinessLayer.Model.HotelItinerary>> SearchAsync(BusinessLayer.Model.HotelSearchRQ hotelSearchReq)
        {
            IHotelConnector hotelConnector = Factory.Get<IHotelConnector>() as IHotelConnector;
            StaticConnectorConfiguration configurationService = new StaticConnectorConfiguration(hotelSearchReq.SearchText,hotelSearchReq.CheckInDate,hotelSearchReq.CheckoutDate);            
            Connector.Model.HotelIteneraryRQ hotelSearchRQ = new Connector.Model.HotelIteneraryRQ()
            {                
                Filters = configurationService.Filters,
                HotelSearchCriterion = configurationService.HotelSearchCriterion,
                PagingInfo = configurationService.PagingInfo,
                ResultRequested = configurationService.ResultRequested,
                SessionId=hotelSearchReq.SessionId.ToString()
            };           

            Task<Connector.Model.HotelIteneraryRS> hotelSearchRS= hotelConnector.SearchHotelsAsync(hotelSearchRQ);
            var result = new List<BusinessLayer.Model.HotelItinerary>();

            foreach (var itinerary in hotelSearchRS.Result.HotelItineraries)
            {
                var hotel = new BusinessLayer.Model.Hotel()
                {
                    HotelId = itinerary.HotelProperty.Id,
                    HotelName = itinerary.HotelProperty.Name,
                    Address = itinerary.HotelProperty.Address.CompleteAddress,
                    StarRating = itinerary.HotelProperty.HotelRating.Rating
                };
                List<Uri> urls = new List<Uri>();
                foreach (var media in itinerary.HotelProperty.MediaContent)
                {
                    urls.Add(new Uri(media.Url));
                }
                BusinessLayer.Model.HotelItinerary hotelItinerary = new BusinessLayer.Model.HotelItinerary()
                {
                    Hotel = hotel,
                    BaseFare = itinerary.Fare.BaseFare.Amount,
                    MediaUri = urls
                };
                result.Add(hotelItinerary);
            }             
            return result;
        }      
    }
}
