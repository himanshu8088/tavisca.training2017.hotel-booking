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
                SessionId = Guid.Parse("b7f8e952-9825-4d44-9262-2d5907f5d600"),
                CheckInDate = DateTime.Now.AddDays(1),
                CheckOutDate = DateTime.Now.AddDays(5),
                GuestCount = 1,
                HotelId = 258057,
                Location = new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 27.16973f,
                    Longitude = 27.16973f
                },
                NoOfRooms = 1,
                SearchText = "Pune",
                RoomId= Guid.Parse("8979b327-96e6-424f-81c0-dd9594841fdf")
            };
            HotelConnector hotelConnector = new HotelConnector();

            //Act
            var response = await hotelConnector.SearchPriceAsync(priceSearchRQ);

            //Assert
            Assert.NotNull(response);
        }
    }
}
