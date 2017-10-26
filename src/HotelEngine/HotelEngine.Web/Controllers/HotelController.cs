using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using HotelEngine.Services;
using System;

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
        public async Task<IActionResult> SearchHotelsAsync([FromBody] HotelSearchRQ searchRQ)
        {
            HotelSearchRS hotelSearchRS=null;
            try
            {
                hotelSearchRS = await _hotelService.SearchHotelsAsync(searchRQ);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }            
            return Ok(hotelSearchRS);
        }

        [HttpPost("roomsearch")]
        public async Task<IActionResult> SearchRoomsAsync([FromBody] RoomSearchRQ roomRQ)
        {
            RoomSearchRS roomSearchRS = null;
            roomSearchRS=await _hotelService.RoomSearchAsync(roomRQ);
            try
            {
                roomSearchRS = await _hotelService.RoomSearchAsync(roomRQ);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
            return Ok(roomSearchRS);
        }

        [HttpPost("price")]
        public async Task<IActionResult> SearchRoomPriceAsync([FromBody] RoomPriceSearchRQ priceRQ)
        {
            RoomPriceSearchRS roomPriceSearchRS = null;           
            try
            {
                roomPriceSearchRS = await _hotelService.RoomPriceSearchAsync(priceRQ);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
            return Ok(roomPriceSearchRS);     
        }
        [HttpPost("book")]
        public async Task<IActionResult> BookRoomAsync([FromBody] RoomBookRQ roomBookRQ)
        {

            RoomBookRS roomBookRS = null;
            try
            {
                roomBookRS = await _hotelService.BookRoomAsync(roomBookRQ);
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }            
            return Ok(roomBookRS);
        }
    }
}
