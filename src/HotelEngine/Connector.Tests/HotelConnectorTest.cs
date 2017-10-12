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

        [Fact]
        public async void Price_Search_Test()
        {
            //Arrange           
            var priceSearchRQ = new RoomPriceSearchRQ()
            {
                SessionId = Guid.Parse("fbe2f93e-8d56-4cd8-9e76-24e01e8ea171"),
                CheckInDate = DateTime.Parse("26-10-2017"),
                CheckOutDate = DateTime.Parse("28-10-2017"),
                GuestCount = 2,
                HotelId = 252448,
                Location = new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 27.173891f,
                    Longitude = 78.042068f
                },
                NoOfRooms = 1,
                SearchText = "Pune",
                RoomId= Guid.Parse("a4184f5e-a7e2-4167-8582-b7b56e1d3825")
            };
            HotelConnector hotelConnector = new HotelConnector();

            //Act
            var response = await hotelConnector.SearchPriceAsync(priceSearchRQ);

            //Assert
            Assert.NotNull(response);
        }
    }
}
