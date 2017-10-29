using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using HotelEngine.Services;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HotelEngine.Web.Controllers
{
    [Route("[controller]")]
    public class HotelController : Controller
    {
        private IHotelService _hotelService;
        private ILoggerFactory _loggerFactory;
        private ILogger _logger;

        public HotelController(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _loggerFactory.AddFile($"Log/log-{Guid.NewGuid()}.txt");            
            _hotelService = new HotelService();           
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchHotelsAsync([FromBody] HotelSearchRQ hotelSearchRQ)
        {
            HotelSearchRS hotelSearchRS=null;
            try
            {
                _logger = _loggerFactory.CreateLogger<HotelSearchRQ>();
                _logger.LogInformation("{@info}", hotelSearchRQ);
                hotelSearchRS = await _hotelService.SearchHotelsAsync(hotelSearchRQ);
                _logger = _loggerFactory.CreateLogger<HotelSearchRS>();
                _logger.LogInformation("{@info}", hotelSearchRS);
            }
            catch (Exception e)
            {
                _logger = _loggerFactory.CreateLogger<Exception>();
                _logger.LogError("{@exception}", e);
                return StatusCode(500);
            }            
            return Ok(hotelSearchRS);
        }

        [HttpPost("roomsearch")]
        public async Task<IActionResult> SearchRoomsAsync([FromBody] RoomSearchRQ roomSearchRQ)
        {
            RoomSearchRS roomSearchRS = null;
            roomSearchRS=await _hotelService.RoomSearchAsync(roomSearchRQ);
            try
            {
                _logger = _loggerFactory.CreateLogger<RoomSearchRQ>();
                _logger.LogInformation("{@info}", roomSearchRQ);
                roomSearchRS = await _hotelService.RoomSearchAsync(roomSearchRQ);
                _logger = _loggerFactory.CreateLogger<RoomSearchRS>();
                _logger.LogInformation("{@info}", roomSearchRS);
            }
            catch (Exception e)
            {
                _logger = _loggerFactory.CreateLogger<Exception>();
                _logger.LogError("{@exception}", e);
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
