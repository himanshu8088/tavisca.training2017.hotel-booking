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
        public async Task<List<BusinessLayer.Model.HotelItinerary>> SearchAsync(BusinessLayer.Model.HotelSearchRQ searchRQ)
        {
            IHotelConnector hotelConnector = Factory.Get<IHotelConnector>() as IHotelConnector;
            StaticConnectorConfiguration configurationService = new StaticConnectorConfiguration(searchRQ.SearchText,searchRQ.CheckInDate,searchRQ.CheckoutDate,searchRQ.PsgCount,searchRQ.NoOfRooms,searchRQ.Location.Latitude,searchRQ.Location.Longitude);            
            Connector.Model.HotelIteneraryRQ hotelSearchRQ = new Connector.Model.HotelIteneraryRQ()
            {                
                Filters = configurationService.Filters,
                HotelSearchCriterion = configurationService.HotelSearchCriterion,
                PagingInfo = configurationService.PagingInfo,
                ResultRequested = configurationService.ResultRequested,
                SessionId=searchRQ.SessionId.ToString()
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
                var loc = new BusinessLayer.Model.Location()
                {
                    Latitude=itinerary.HotelProperty.GeoCode.Latitude,
                    Longitude= itinerary.HotelProperty.GeoCode.Longitude
                };
                
                BusinessLayer.Model.HotelItinerary hotelItinerary = new BusinessLayer.Model.HotelItinerary()
                {
                    Hotel = hotel,
                    BaseFare = itinerary.Fare.BaseFare.Amount,
                    MediaUri = urls, 
                    Location=loc                    
                };
                result.Add(hotelItinerary);
            }             
            return result;
        }      
    }
}
