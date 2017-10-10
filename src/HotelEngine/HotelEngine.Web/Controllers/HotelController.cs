using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using HotelEngine.Services.Factories;

namespace HotelEngine.Web.Controllers
{
    [Route("[controller]")]
    public class HotelController : Controller
    {
        [HttpPost("search")]
        public async Task<IActionResult> SearchAsync([FromBody] HotelSearchRQ searchRQ)
        {
            IHotelService hotelService = Factory.Get<IHotelService>() as IHotelService;
            var hotels = await hotelService.SearchHotelsAsync(searchRQ);
            return Ok(hotels);
        }

        [HttpPost("roomsearch")]
        public async Task<IActionResult> RoomSearchAsync([FromBody] RoomSearchRQ roomRQ)
        {
            IHotelService hotelService = Factory.Get<IHotelService>() as IHotelService;
            var response = await hotelService.RoomSearchAsync(roomRQ);
            return Ok(response);
        }
    }
}
