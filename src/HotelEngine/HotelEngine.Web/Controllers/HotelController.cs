using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using HotelEngine.Services;

namespace HotelEngine.Web.Controllers
{
    [Route("[controller]")]
    public class HotelController : Controller
    {
        private IHotelService _hotelService;
        public HotelController()
        {
            _hotelService = new HotelService();
        }


        [HttpPost("search")]
        public async Task<IActionResult> SearchAsync([FromBody] HotelSearchRQ searchRQ)
        {           
            var hotels = await _hotelService.SearchHotelsAsync(searchRQ);
            return Ok(hotels);
        }

        [HttpPost("roomsearch")]
        public async Task<IActionResult> RoomSearchAsync([FromBody] RoomSearchRQ roomRQ)
        {            
            var response = await _hotelService.RoomSearchAsync(roomRQ);
            return Ok(response);
        }
    }
}
