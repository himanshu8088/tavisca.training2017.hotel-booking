using System.Threading.Tasks;
using System;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using HotelEngine.Adapter;

namespace HotelEngine.Core.Implementation
{
    public class HotelSearch : IHotelSearch
    {
        private IHotelConnector _hotelConnector;

        public HotelSearch()
        {
            _hotelConnector = new HotelConnector();
        }

        public async Task<HotelSearchRS> SearchAsync(HotelSearchRQ hotelSearchRequest)
        {
            var hotelSearchRS = await _hotelConnector.SearchHotelsAsync(hotelSearchRequest);
            return hotelSearchRS;
        }
        //public async Task<List<HotelItinerary>> SearchAsync(HotelSearchRQ hotelSearchRQ)
        //{
        //IHotelSearch hotelSearch = Factory.Get<IHotelSearch>() as IHotelSearch;


        //StaticConnectorConfiguration configurationService = new StaticConnectorConfiguration();
        //HotelsAvailConfig hotelsAvailConfig= configurationService.GetHotelsAvailConfig( searchRQ.CheckInDate, searchRQ.CheckoutDate, searchRQ.SearchText, searchRQ.PsgCount, searchRQ.NoOfRooms, searchRQ.Location.Latitude, searchRQ.Location.Longitude);
        //Connector.Model.HotelIteneraryRQ hotelSearchRQ = new Connector.Model.HotelIteneraryRQ()
        //{                
        //    Filters = hotelsAvailConfig.Filters,
        //    HotelSearchCriterion = hotelsAvailConfig.HotelSearchCriterion,
        //    PagingInfo = hotelsAvailConfig.PagingInfo,
        //    ResultRequested = hotelsAvailConfig.ResultRequested,
        //    SessionId=searchRQ.SessionId.ToString()
        //};           

        //var hotelSearchRS = await hotelSearch.SearchAsync(hotelSearchRQ);
        //var result = new List<HotelItinerary>();
        ////var i = 0;
        //foreach (var itinerary in hotelSearchRS.HotelItineraries)
        //{
        //    //var roomPrice=itinerary.Rooms[i].StdRoomRate.TotalFare.Amount;
        //    //i++;
        //    var hotel = new BusinessLayer.Model.Hotel()
        //    {
        //        HotelId = itinerary.HotelProperty.Id,
        //        HotelName = itinerary.HotelProperty.Name,
        //        Address = itinerary.HotelProperty.Address.CompleteAddress,
        //        StarRating = itinerary.HotelProperty.HotelRating.Rating
        //    };
        //    List<Uri> urls = new List<Uri>();
        //    foreach (var media in itinerary.HotelProperty.MediaContent)
        //    {
        //        urls.Add(new Uri(media.Url));
        //    }
        //    var loc = new BusinessLayer.Model.Location()
        //    {
        //        Latitude=itinerary.HotelProperty.GeoCode.Latitude,
        //        Longitude= itinerary.HotelProperty.GeoCode.Longitude
        //    };

        //    BusinessLayer.Model.HotelItinerary hotelItinerary = new BusinessLayer.Model.HotelItinerary()
        //    {
        //        Hotel = hotel,
        //        BaseFare = itinerary.Fare.BaseFare.Amount,
        //        MediaUri = urls, 
        //        Location=loc                    
        //    };
        //    result.Add(hotelItinerary);


        //return result;
        //}    
    }
}

