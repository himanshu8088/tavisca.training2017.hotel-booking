using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Model;
using Services.Contracts;
using Services.Factory;
using Tavisca.Training2017.HotelBooking.Web.Models;
using System.Collections.Generic;
using System.Globalization;

namespace Tavisca.Training2017.HotelBooking.Web.Controllers
{
    [Route("[controller]")]
    public class HotelController : Controller
    {
        [HttpPost("search")]
        public async Task<IActionResult> SearchAsync([FromBody] SearchRQ searchRQ)
        {
            IHotelService hotelService = Factory.Get<IHotelService>() as IHotelService;
            List<Hotel> hotels = null;

            HotelSearchRQ hotelSearchRequest = new HotelSearchRQ()
            {
                SearchText = searchRQ.SearchText,
                CheckInDate = DateTime.Parse(searchRQ.CheckInDate),
                CheckOutDate = DateTime.Parse(searchRQ.CheckOutDate),
                Location =new Services.Model.Location()
                {
                    Latitude=searchRQ.Location.Latitude,
                    Longitude=searchRQ.Location.Longitude
                },
                NoOfRooms=searchRQ.NoOfRooms,
                PsgCount= searchRQ.GuestCount
            };

            Task<List<Hotel>> response = hotelService.SearchHotelsAsync(hotelSearchRequest);
            hotels = response.GetAwaiter().GetResult();
            return Ok(hotels);
        }

        [HttpPost("roomsearch")]
        public async Task<IActionResult> RoomSearchAsync([FromBody] RoomRQ roomRQ)
        {
            IHotelService hotelService = Factory.Get<IHotelService>() as IHotelService;

            var roomSearchRequest = new Services.Model.RoomSearchRQ()
            {
                SearchText = roomRQ.SearchText,
                CheckinDate = DateTime.Parse(roomRQ.CheckInDate),
                CheckoutDate = DateTime.Parse(roomRQ.CheckOutDate),
                Location = new Services.Model.Location()
                {
                    Latitude = roomRQ.Location.Latitude,
                    Longitude = roomRQ.Location.Longitude
                },
                NoOfRooms = roomRQ.NoOfRooms,
                PsgCount = roomRQ.GuestCount,
                HotelId = roomRQ.HotelId
            };

            var response = await hotelService.RoomSearchAsync(roomSearchRequest);
            return Ok(response);
        }
    }
}
