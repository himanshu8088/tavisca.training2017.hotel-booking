using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> SearchHotelsAsync([FromBody] HotelSearchRQ searchRQ)
        {           
            var hotels = await _hotelService.SearchHotelsAsync(searchRQ);
            return Ok(hotels);
        }

        [HttpPost("roomsearch")]
        public async Task<IActionResult> SearchRoomsAsync([FromBody] RoomSearchRQ roomRQ)
        {            
            var rooms = await _hotelService.RoomSearchAsync(roomRQ);
            return Ok(rooms);
        }

        [HttpPost("price")]
        public async Task<IActionResult> SearchRoomPriceAsync([FromBody] RoomPriceSearchRQ priceRQ)
        {
            var priceRS = await _hotelService.RoomPriceSearchAsync(priceRQ);
            return Ok(priceRS);
        }
        [HttpPost("book")]
        public async Task<IActionResult> BookRoomAsync([FromBody] RoomBookRQ roomBookRQ)
        {
            var response = await _hotelService.BookRoomAsync(roomBookRQ);
            return Ok(response);
        }
    }
}
