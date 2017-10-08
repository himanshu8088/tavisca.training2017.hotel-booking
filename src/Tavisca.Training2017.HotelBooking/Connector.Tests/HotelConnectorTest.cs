using System;
using Xunit;
using Connector;
using BusinessLayer;
using BusinessLayer.Configuration;

namespace Tavisca.Training2017.HotelBookingWeb.Tests
{
    public class HotelControllerTest
    {
        [Fact]
        public void Search_Should_Give_Valid_Result_When_SearchRQ_Is_Complete()
        {
            //Arrange
            HotelConnector hotelConnector = new HotelConnector();
            StaticConnectorConfiguration configurationService = new StaticConnectorConfiguration();            
            var hotelsConfig = configurationService.GetHotelsAvailConfig(DateTime.Now, DateTime.Now.AddDays(7), "Pune");
            Connector.Model.HotelIteneraryRQ hotelSearchRQ = new Connector.Model.HotelIteneraryRQ()
            {
                SessionId=Guid.NewGuid().ToString(),
                Filters = hotelsConfig.Filters,
                HotelSearchCriterion = hotelsConfig.HotelSearchCriterion,
                PagingInfo = hotelsConfig.PagingInfo,
                ResultRequested = hotelsConfig.ResultRequested                
            };
            
            //Act
            var result= hotelConnector.SearchHotelsAsync(hotelSearchRQ);

            //Assert
            Assert.NotNull(result);            
        } 
        
        [Fact]
        public void Search_Room()
        {
            RoomsAvailConfig roomsAvailConfig = new RoomsAvailConfig();
            HotelsAvailConfig hotelsAvailConfig = new HotelsAvailConfig("Pune",DateTime.Now,DateTime.Now.AddDays(7));
            HotelEngineClient client = new HotelEngineClient();
            var req = new HotelRoomAvailRQ()
            {
                SessionId=Guid.NewGuid().ToString(),
                HotelSearchCriterion = hotelsAvailConfig.HotelSearchCriterion,
                Itinerary = roomsAvailConfig.HotelItinerary,
                ResultRequested = roomsAvailConfig.ResultRequested,
            };
            var res=client.HotelRoomAvailAsync(req);
            var result = res.Result;
        }
    }
}
