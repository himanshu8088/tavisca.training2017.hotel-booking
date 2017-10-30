using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelEngine.Contracts.Contracts;
using HotelEngine.Contracts.Models;
using HotelEngine.Services;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HotelEngine.Web.Controllers
{
    [Route("[controller]")]
    public class HotelController : Controller
    {        
        private IHotelService _hotelService;
        private ILogger _logger;

        public HotelController(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            var logFilePath=configuration.GetSection("LogFilePath").Value;
            var logfileName = $"Logs\\{Guid.NewGuid()}.log";
            var logFile = Path.Combine(logFilePath, logfileName);
            loggerFactory.AddFile(logFile);
            _logger = loggerFactory.CreateLogger("HostLogger");
            _hotelService = new HotelService();           
        }

        /// <summary>
        /// Search hotels asynchronously
        /// </summary>
        
        [HttpPost("search")]
        public async Task<IActionResult> SearchHotelsAsync([FromBody] HotelSearchRQ hotelSearchRQ)
        {
            HotelSearchRS hotelSearchRS=null;
            try
            {
                _logger.LogInformation("{@info}", hotelSearchRQ);                
                hotelSearchRS = await _hotelService.SearchHotelsAsync(hotelSearchRQ);
                _logger.LogInformation("{@info}", hotelSearchRS);
            }
            catch (Exception e)
            {
                _logger.LogError("{@error}", e);
                return StatusCode(500);
            }            
            return Ok(hotelSearchRS);
        }


        /// <summary>
        /// Search rooms within hotels asynchronously
        /// </summary>        

        [HttpPost("roomsearch")]
        public async Task<IActionResult> SearchRoomsAsync([FromBody] RoomSearchRQ roomSearchRQ)
        {            
            RoomSearchRS roomSearchRS = null;
            roomSearchRS=await _hotelService.RoomSearchAsync(roomSearchRQ);
            try
            {
                _logger.LogInformation("{@info}", roomSearchRQ);
                roomSearchRS = await _hotelService.RoomSearchAsync(roomSearchRQ);
                _logger.LogInformation("{@info}", roomSearchRS);
            }
            catch (Exception e)
            {
                _logger.LogError("{@error}", e);
                return StatusCode(500);
            }
            return Ok(roomSearchRS);
        }

        /// <summary>
        /// Search hotel room price asynchronously
        /// </summary>        

        [HttpPost("price")]
        public async Task<IActionResult> SearchRoomPriceAsync([FromBody] RoomPriceSearchRQ roomPriceSearchRQ)
        {            
            RoomPriceSearchRS roomPriceSearchRS = null;           
            try
            {
                _logger.LogInformation("{@info}", roomPriceSearchRQ);
                roomPriceSearchRS = await _hotelService.RoomPriceSearchAsync(roomPriceSearchRQ);
                _logger.LogInformation( "{@info}", roomPriceSearchRS);
            }
            catch (Exception e)
            {
                _logger.LogError("{@error}", e);
                return StatusCode(500);
            }
            return Ok(roomPriceSearchRS);     
        }

        /// <summary>
        /// Book selected room asynchronously
        /// </summary>

        [HttpPost("book")]
        public async Task<IActionResult> BookRoomAsync([FromBody] RoomBookRQ roomBookRQ)
        {            
            RoomBookRS roomBookRS = null;
            try
            {
                _logger.LogInformation( "{@info}", roomBookRQ);
                roomBookRS = await _hotelService.BookRoomAsync(roomBookRQ);
                _logger.LogInformation( "{@info}", roomBookRS);
            }
            catch(Exception e)
            {
                _logger.LogError( "{@error}", e);
                return StatusCode(500);
            }            
            return Ok(roomBookRS);
        }
    }
}
