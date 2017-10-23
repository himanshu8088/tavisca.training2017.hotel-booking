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
                SessionId = Guid.NewGuid(),
                CheckInDate = DateTime.Parse("24-10-2017"),
                CheckOutDate = DateTime.Parse("25-10-2017"),
                GuestCount = 1,                
                Location = new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 36.110931f,
                    Longitude = -115.169346f
                },
                NoOfRooms = 1,
                SearchText = ""
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
                SessionId = Guid.NewGuid(),
                CheckInDate = DateTime.Parse("14-11-2017"),
                CheckOutDate = DateTime.Parse("15-11-2017"),
                GuestCount = 1,
                Location = new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 13.1918249f,
                    Longitude = 77.64561f
                },
                NoOfRooms = 1,
                SearchText = "",
                HotelId = 2038272,
                RoomName = "SINGLE SUPERIOR - BED AND BREAKFAST"
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
                SessionId = Guid.NewGuid(),
                CheckInDate = DateTime.Parse("25-10-2017"),
                CheckOutDate = DateTime.Parse("26-10-2017"),
                GuestCount = 1,
                Location = new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 35.68855f,
                    Longitude = -105.9421f
                },
                NoOfRooms = 1,
                SearchText = "",
                HotelId= 60982,
                RoomName = "Standard",
                GuestDetail = new UserDetail()
                {
                    DOB = DateTime.Parse("21-11-1994"),
                    EmailId = "test@gmail.com",
                    FirstName = "test",
                    LastName = "user",
                    MobileNo = "12345"
                },
                CardDetail = new CardDetail()
                {
                    CardHolderName = "test user",
                    CardNumber = "123456789012345",
                    CVV = 123,
                    ExpiryDate = DateTime.Parse("21-11-2022")
                }
            };
            HotelConnector hotelConnector = new HotelConnector();

            //Act
            var response = await hotelConnector.BookRoomAsync(roomBookRQ);

            //Assert
            Assert.NotNull(response);
        }
    }
}
