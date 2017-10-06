﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Model;
using Services.Contracts;
using Services.Factory;
using Tavisca.Training2017.HotelBooking.Models;
using System.Collections.Generic;
using System.Globalization;

namespace Tavisca.Training2017.HotelBooking.Controllers
{
    [Route("[controller]")]
    public class HotelController : Controller
    {
        [HttpPost("search.html")]
        public async Task<IActionResult> SearchAsync([FromBody] SearchRQ searchRQ)
        {
            IHotelService hotelService = Factory.Get<IHotelService>() as IHotelService;
            List<Hotel> hotels = null;

            HotelSearchRQ hotelSearchRequest = new HotelSearchRQ()
            {
                SearchText = searchRQ.SearchText,
                CheckInDate =  DateTime.Parse(searchRQ.CheckInDate),
                CheckOutDate = DateTime.Parse(searchRQ.CheckOutDate)
            };

            Task<List<Hotel>> response = hotelService.SearchHotelsAsync(hotelSearchRequest);
            hotels = response.Result;
            return Ok(hotels);
        }
    }
}
