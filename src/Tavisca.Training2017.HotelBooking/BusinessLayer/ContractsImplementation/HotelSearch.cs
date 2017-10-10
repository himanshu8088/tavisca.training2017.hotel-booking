using BusinessLayer.Contracts;
using Connector.Contracts;
using System.Threading.Tasks;
using BusinessLayer.Factories;
using System;
using System.Collections.Generic;
using BusinessLayer.Configuration;

namespace BusinessLayer.ContractsImplementation
{
    public class HotelSearch : IHotelSearch
    {
        public async Task<List<BusinessLayer.Model.HotelItinerary>> SearchAsync(BusinessLayer.Model.HotelSearchRQ searchRQ)
        {
            IHotelConnector hotelConnector = Factory.Get<IHotelConnector>() as IHotelConnector;
            StaticConnectorConfiguration configurationService = new StaticConnectorConfiguration();
            HotelsAvailConfig hotelsAvailConfig= configurationService.GetHotelsAvailConfig( searchRQ.CheckInDate, searchRQ.CheckoutDate, searchRQ.SearchText, searchRQ.PsgCount, searchRQ.NoOfRooms, searchRQ.Location.Latitude, searchRQ.Location.Longitude);
            Connector.Model.HotelIteneraryRQ hotelSearchRQ = new Connector.Model.HotelIteneraryRQ()
            {                
                Filters = hotelsAvailConfig.Filters,
                HotelSearchCriterion = hotelsAvailConfig.HotelSearchCriterion,
                PagingInfo = hotelsAvailConfig.PagingInfo,
                ResultRequested = hotelsAvailConfig.ResultRequested,
                SessionId=searchRQ.SessionId.ToString()
            };           

            Task<Connector.Model.HotelIteneraryRS> hotelSearchRS= hotelConnector.SearchHotelsAsync(hotelSearchRQ);
            var result = new List<BusinessLayer.Model.HotelItinerary>();
            //var i = 0;
            foreach (var itinerary in hotelSearchRS.GetAwaiter().GetResult().HotelItineraries)
            {
                //var roomPrice=itinerary.Rooms[i].StdRoomRate.TotalFare.Amount;
                //i++;
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
