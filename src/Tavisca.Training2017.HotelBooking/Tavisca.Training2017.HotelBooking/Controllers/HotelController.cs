using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tavisca.Training2017.HotelBooking.Models;
using Tavisca.Training2017.HotelBooking.Factories;
using Engines;


namespace Tavisca.Training2017.HotelBooking.Controllers
{
    [Route("[hotel]")]
    public class HotelController : Controller
    {
        [HttpPost("search")]
        public IActionResult Search([FromBody]SearchRQ req)
        {
            HotelEngineClient client = new HotelEngineClient();            
            var hotelSearchRQ = new SearchRQFactory().CreateCompleteRQ(req);
            var response=client.HotelAvailAsync(hotelSearchRQ);            
            var result = response.Result;            
            return Ok();
        }
    }
}
