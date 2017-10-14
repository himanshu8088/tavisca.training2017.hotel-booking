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
        public async void Hotel_Search_Test()
        {
            //Arrange           
            var hotelSearchRQ = new HotelEngine.Contracts.Models.HotelSearchRQ()
            {
                SessionId = Guid.Parse("b7f8e952-9825-4d44-9262-2d5907f5d600"),
                CheckInDate = DateTime.Now.AddDays(1),
                CheckOutDate = DateTime.Now.AddDays(5),
                GuestCount = 1,                
                Location = new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 27.16973f,
                    Longitude = 78.042068f
                },
                NoOfRooms = 1,
                SearchText = "Pune"
            };
            HotelConnector hotelConnector = new HotelConnector();

            //Act
            var response = await hotelConnector.SearchHotelsAsync(hotelSearchRQ);

            //Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async void Room_Search_Test()
        {
            //Arrange           
            var roomSearchRQ = new RoomSearchRQ()
            {
                SessionId = Guid.Parse("b7f8e952-9825-4d44-9262-2d5907f5d600"),
                CheckInDate = DateTime.Now.AddDays(1),
                CheckOutDate = DateTime.Now.AddDays(5),
                GuestCount = 1,
                HotelId = 258057,
                Location = new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 27.16973f,
                    Longitude = 78.042068f
                },
                NoOfRooms = 1,
                SearchText = "Pune",               
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
                SessionId = Guid.Parse("ba317ee7-b8ba-4af6-aa0a-a17dba95ecaa"),
                CheckInDate = DateTime.Parse("21-11-2017"),
                CheckOutDate = DateTime.Parse("22-11-2017"),
                GuestCount = 2,
                HotelId = 258057,
                Location = new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 27.16084f,
                    Longitude = 27.16084f
                },
                NoOfRooms = 1,
                SearchText = "Pune",
                RoomId = Guid.Parse("a4184f5e-a7e2-4167-8582-b7b56e1d3825"),
                RoomName= "Superior Room"

            };
            HotelConnector hotelConnector = new HotelConnector();

            //Act
            var response = await hotelConnector.SearchPriceAsync(priceSearchRQ);

            //Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async void Booking_Test()
        {
            //Arrange           
            var roomBookRQ = new RoomBookRQ()
            {
                SessionId = Guid.Parse("ba317ee7-b8ba-4af6-aa0a-a17dba95ecaa"),
                CheckInDate = DateTime.Parse("2017-10-25T00:00:00"),
                CheckOutDate = DateTime.Parse("2017-10-26T00:00:00"),
                GuestCount = 2,
                HotelId = 88359,
                Location = new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 36.1732178f,
                    Longitude = -115.144829f
                },
                NoOfRooms = 1,
                SearchText = "California Hotel and Casino",
                RoomId = Guid.Parse("a4184f5e-a7e2-4167-8582-b7b56e1d3825"),
                RoomName = "Deluxe"
            };
            HotelConnector hotelConnector = new HotelConnector();

            //Act
            var response = await hotelConnector.BookRoomAsync(roomBookRQ);

            //Assert
            Assert.NotNull(response);
        }
    }
}
