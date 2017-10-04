using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tavisca.Training2017.HotelBooking.Models;

namespace Tavisca.Training2017.HotelBooking.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {        
        [HttpGet("hotel_search")]        
        public IActionResult Search([FromQuery]SearchRQ req)
        {
            return Ok();
        }        
    }
}
