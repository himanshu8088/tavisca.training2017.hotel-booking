using HotelEngine.Adapter;
using HotelEngine.Adapter.Configuration;
using HotelEngine.Contracts.Models;
using HotelSearch;
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
                SessionId = Guid.Parse("943b6ebb-65a3-4abc-88c5-e65a02b16824"),
                CheckInDate = DateTime.Now.AddDays(1),
                CheckOutDate = DateTime.Now.AddDays(5),
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
