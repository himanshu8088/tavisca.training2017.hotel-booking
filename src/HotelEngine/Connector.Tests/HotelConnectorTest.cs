using HotelEngine.Adapter;
using HotelEngine.Adapter.Configuration;
using HotelEngine.Contracts.Models;
using Proxies;
using System;
using System.Threading.Tasks;
using Xunit;


namespace Tavisca.Training2017.HotelBookingWeb.Tests
{
    public class HotelControllerTest
    {



        [Fact]
        public async void Room_Search_Test()
        {
            //Arrange           
            var roomSearchRQ = new RoomSearchRQ()
            {
                SessionId = Guid.Parse("b7f8e952-9825-4d44-9262-2d5907f5d600"),
                CheckInDate=DateTime.Now.AddDays(1),
                CheckOutDate=DateTime.Now.AddDays(5),
                GuestCount=1,
                HotelId= 258057,
                Location=new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 27.16973f,
                    Longitude= 27.16973f
                },
                NoOfRooms=1,
                SearchText="Pune"
            };
            HotelConnector hotelConnector = new HotelConnector();

            //Act
            var response = await hotelConnector.SearchRoomsAsync(roomSearchRQ);

            //Assert
            Assert.NotNull(response);

        }

        //[Fact]
        //public void Search_Should_Give_Valid_Result_When_SearchRQ_Is_Complete()
        //{
        //    //Arrange
        //    HotelConnector hotelConnector = new HotelConnector();
        //    StaticConnectorConfiguration configurationService = new StaticConnectorConfiguration();
        //    var hotelsConfig = configurationService.GetHotelsAvailConfig(DateTime.Now, DateTime.Now.AddDays(7), "Pune");
        //    Connector.Model.HotelIteneraryRQ hotelSearchRQ = new Connector.Model.HotelIteneraryRQ()
        //    {
        //        SessionId = Guid.NewGuid().ToString(),
        //        Filters = hotelsConfig.Filters,
        //        HotelSearchCriterion = hotelsConfig.HotelSearchCriterion,
        //        PagingInfo = hotelsConfig.PagingInfo,
        //        ResultRequested = hotelsConfig.ResultRequested
        //    };

        //    //Act
        //    var result = hotelConnector.SearchHotelsAsync(hotelSearchRQ);

        //    //Assert
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public void Search_Room()
        //{
        //    RoomsAvailConfig roomsAvailConfig = new RoomsAvailConfig();
        //    HotelsAvailConfig hotelsAvailConfig = new HotelsAvailConfig("Pune", DateTime.Now, DateTime.Now.AddDays(7));
        //    HotelEngineClient client = new HotelEngineClient();
        //    var req = new HotelRoomAvailRQ()
        //    {
        //        SessionId = Guid.NewGuid().ToString(),
        //        HotelSearchCriterion = hotelsAvailConfig.HotelSearchCriterion,
        //        Itinerary = roomsAvailConfig.HotelItinerary,
        //        ResultRequested = roomsAvailConfig.ResultRequested,
        //    };
        //    var res = client.HotelRoomAvailAsync(req);
        //    var result = res.Result;
        //}
    }
}
