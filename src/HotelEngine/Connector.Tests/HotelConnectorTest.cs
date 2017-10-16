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
                RoomName = "Superior Room"

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
                //SessionId = Guid.Parse("ba317ee7-b8ba-4af6-aa0a-a17dba95ecaa"),
                //CheckInDate = DateTime.Parse("21-11-2017"),
                //CheckOutDate = DateTime.Parse("22-11-2017"),
                //GuestCount = 2,
                //HotelId = 252448,
                //Location = new HotelEngine.Contracts.Models.Location()
                //{
                //    Latitude = 27.16084f,
                //    Longitude = 27.16084f
                //},
                //NoOfRooms = 1,
                //SearchText = "Pune",
                //RoomName = "DOUBLE STANDARD - BED AND BREAKFAST",
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
                RoomName = "Superior Room",
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
