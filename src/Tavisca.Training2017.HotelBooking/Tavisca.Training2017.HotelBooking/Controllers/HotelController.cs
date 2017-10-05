using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Model;
using Services.Contracts;
using Services.Factory;

namespace Tavisca.Training2017.HotelBooking.Controllers
{
    [Route("[controller]")]
    public class HotelController : Controller
    {
        [HttpPost("search")]
        public async Task<IActionResult>  SearchAsync([FromBody]HotelSearchRequest hotelSearchRQ)
        {
            IHotelService hotelService = Factory.Get<IHotelService>() as IHotelService;
            HotelSearchResult hotelSearchResult = null;

            HotelSearchRequest hotelSearchRequest = new HotelSearchRequest()
            {
                SearchText = hotelSearchRQ.SearchText,
                CheckInDate = hotelSearchRQ.CheckInDate,
                CheckOutDate = hotelSearchRQ.CheckOutDate,
                PosId = 101
            };
            try
            {
                hotelSearchResult = await hotelService.SearchHotelsAsync(hotelSearchRequest);                
            }
            catch (Exception e)
            {

            }
            return Ok();
        }
    }
}
