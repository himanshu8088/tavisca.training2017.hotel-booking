using HotelEngine.Adapter;
using HotelEngine.Adapter.Configuration;
using HotelEngine.Contracts.Models;
using Proxies;
using System;
using System.Threading.Tasks;
using Xunit;


namespace Tavisca.Training2017.HotelBookingWeb.Tests
{
    public class HotelAdapterTest
    {
        [Fact]
        public async void Hotel_Search_Test()
        {
            //Arrange           
            var hotelSearchRQ = new HotelEngine.Contracts.Models.HotelSearchRQ()
            {
                SessionId = Guid.NewGuid(),
                CheckInDate = DateTime.Now.AddDays(15),
                CheckOutDate = DateTime.Now.AddDays(17),
                GuestCount = 1,                
                Location = new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 18.5967712f,
                    Longitude = 73.74219f
                },
                NoOfRooms = 1,
                SearchText = "Orritel Hotel Pune"
            };
            HotelAdapter hotelConnector = new HotelAdapter();

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
                SessionId = Guid.NewGuid(),
                CheckInDate = DateTime.Now.AddDays(15),
                CheckOutDate = DateTime.Now.AddDays(17),
                GuestCount = 1,
                HotelId = 287948,
                Location = new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 18.5967712f,
                    Longitude = 73.74219f
                },
                NoOfRooms = 1,
                SearchText = "Orritel Hotel Pune",
            };
            HotelAdapter hotelConnector = new HotelAdapter();

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
                CheckInDate = DateTime.Now.AddDays(15),
                CheckOutDate = DateTime.Now.AddDays(17),
                GuestCount = 1,
                HotelId = 287948,
                Location = new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 18.5967712f,
                    Longitude = 73.74219f
                },
                NoOfRooms = 1,
                SearchText = "Orritel Hotel Pune",
                RoomName = "Suite"
            };
            HotelAdapter hotelConnector = new HotelAdapter();

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
                SessionId =Guid.NewGuid(),
                CheckInDate = DateTime.Now.AddDays(15),
                CheckOutDate = DateTime.Now.AddDays(17),
                GuestCount = 1,
                HotelId = 287948,
                Location = new HotelEngine.Contracts.Models.Location()
                {
                    Latitude = 18.5967712f,
                    Longitude = 73.74219f
                },
                NoOfRooms = 1,
                SearchText = "Orritel Hotel Pune",
                RoomName = "Suite",
                GuestDetail = new UserDetail()
                {
                    DOB = DateTime.Parse("21-11-1994"),
                    EmailId = "hsoni@tavisca.com",
                    FirstName = "testFName",
                    LastName = "testLName",
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
            HotelAdapter hotelConnector = new HotelAdapter();

            //Act
            var response = await hotelConnector.BookRoomAsync(roomBookRQ);

            //Assert
            Assert.NotNull(response);
        }
    }
}
